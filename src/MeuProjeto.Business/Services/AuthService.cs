using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Extensions;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Models.Identity;
using MeuProjeto.Business.Notifications;
using MeuProjeto.Business.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace MeuProjeto.Business.Services
{
    public class AuthService : BaseService<RefreshToken>, IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUserClaimsPrincipalFactory<CustomUser> _userFactory;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(
                            INotifier notifier,
                            IMapper mapper,
                            IUserService userService,
                            IUserClaimsPrincipalFactory<CustomUser> userFactory,
                            IRefreshTokenRepository refreshTokenRepository) : base(notifier, refreshTokenRepository)
        {
            _mapper = mapper;
            _userService = userService;
            _userFactory = userFactory;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress, JwtConfig jwtConfig)
        {
            var refreshToken = await GetRefreshToken(token);

            if (refreshToken == null)
            {
                Notify(new Notification(AuthResources.Errors.RefreshTokenNotFound.Key, AuthResources.Errors.RefreshTokenNotFound.Value));
                return null;
            }

            if (!refreshToken.IsActive)
            {
                Notify(new Notification(AuthResources.Errors.RefreshTokenInactive.Key, AuthResources.Errors.RefreshTokenInactive.Value));
                return null;
            }
            var user = refreshToken.User;
            // replace old refresh token with a new one and save
            var newRefreshToken = GenerateRefreshToken(ipAddress, user.Id, jwtConfig.ExpireMinutes);

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;

            await _refreshTokenRepository.Update(refreshToken);
            await _refreshTokenRepository.Create(newRefreshToken);

            // generate new jwt
            var jwtToken = await GenerateJwtToken(user, jwtConfig);

            return new AuthenticateResponse(jwtToken, DateTime.UtcNow.AddMinutes(jwtConfig.ExpireMinutes), newRefreshToken.Token);
        }

        public async Task<string> GenerateJwtToken(CustomUser user, JwtConfig jwtConfig)
        {
            var claimsPrincipal = await _userFactory.CreateAsync(user);
            var claims = claimsPrincipal.Claims.DistinctBy(c => new { c.Type, c.Value }).ToList();

            return await GenerateJwtToken(user.Id, claims, jwtConfig);
        }

        public async Task<string> GenerateJwtToken(Guid userId, List<Claim> claims, JwtConfig jwtConfig)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()));
            //claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = jwtConfig.Issuer,
                Audience = jwtConfig.ValidOn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddMinutes(jwtConfig.ExpireMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        public async Task<string> CreateRefreshToken(Guid userId, string ipAddress, int expireMinutes)
        {
            var activeRefreshTokens = await _refreshTokenRepository.GetActiveByUserId(userId);

            foreach (var item in activeRefreshTokens)
            {
                item.Revoked = DateTime.UtcNow;
                item.RevokedByIp = ipAddress;

                await _refreshTokenRepository.Update(item);
            }

            var refreshToken = GenerateRefreshToken(ipAddress, userId, expireMinutes);
            await _refreshTokenRepository.Create(refreshToken);

            return refreshToken.Token;
        }

        private RefreshToken GenerateRefreshToken(string ipAddress, Guid userId, int expireMinutes)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress,
                    UserId = userId
                };
            }
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var refreshToken = await GetRefreshToken(token);

            if (!refreshToken.IsActive)
            {
                Notify(new Notification(AuthResources.Errors.RefreshTokenInactive.Key, AuthResources.Errors.RefreshTokenInactive.Value));
                return false;
            }
            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            await _refreshTokenRepository.Update(refreshToken);

            return true;
        }

        private async Task<RefreshToken> GetRefreshToken(string token)
        {
            return await _refreshTokenRepository.GetByToken(token);
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public override void Dispose()
        {
            _refreshTokenRepository?.Dispose();
        }
    }
}