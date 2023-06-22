using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Models.Identity;

namespace MeuProjeto.Business.Interfaces
{
    public interface IAuthService : IDisposable
    {
        public Task<string> GenerateJwtToken(CustomUser user, JwtConfig jwtConfig);
        public Task<string> GenerateJwtToken(Guid userId, List<Claim> claims, JwtConfig jwtConfig);
        public Task<string> CreateRefreshToken(Guid userId, string ipAddress, int expireMinutes);
        public Task<AuthenticateResponse> RefreshToken(string token, string ipAddress, JwtConfig jwtConfig);
        public Task<bool> RevokeToken(string token, string ipAddress);
    }
}