using System;
using System.Linq;
using System.Threading.Tasks;
using MeuProjeto.Api.Extensions;
using MeuProjeto.Api.ViewModels;
using MeuProjeto.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MeuProjeto.Business.Enums;
using MeuProjeto.Business.Notifications;
using MeuProjeto.Business.Resources;
using MeuProjeto.Business.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MeuProjeto.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class AuthController : MainController
    {
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly UserManager<CustomUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IUserClaimsPrincipalFactory<CustomUser> _userFactory;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ICustomLogger _logger;
        private readonly BackgroundWorkerQueue _backgroundWorkerQueue;
        private readonly IServiceScopeFactory _scopeFactory;

        public AuthController(INotifier notificador,
                              SignInManager<CustomUser> signInManager,
                              UserManager<CustomUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IUserService userService,
                              IAuthService authService,
                              IUserClaimsPrincipalFactory<CustomUser> userFactory,
                              ICustomLogger logger,
                              IUser user,
                              BackgroundWorkerQueue backgroundWorkerQueue,
                              IServiceScopeFactory scopeFactory) : base(notificador, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
            _authService = authService;
            _logger = logger;
            _userFactory = userFactory;
            _appSettings = appSettings.Value;
            _backgroundWorkerQueue = backgroundWorkerQueue;
            _scopeFactory = scopeFactory;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userService.CanLogin(loginUser.UserName);
            if (!ValidOperation()) return CustomResponse(loginUser);

            var result = await _signInManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password, false, true);
            if (result.Succeeded)
            {
                return CustomResponse(await GenerateJwt(loginUser.UserName));
            }
            if (result.IsLockedOut)
            {
                NotifyError(new Notification(AuthResources.Errors.UserBlocked.Key, AuthResources.Errors.UserBlocked.Value));
                return CustomResponse(loginUser);
            }

            NotifyError(new Notification(AuthResources.Errors.IncorrectUserPassword.Key, AuthResources.Errors.IncorrectUserPassword.Value));

            return CustomResponse(loginUser);
        }

        [AllowAnonymous]
        [HttpPatch("logout")]
        public async Task<ActionResult> Logout()
        {
            // accept token from request body or cookie
            var token = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
            {
                NotifyError(new Notification(AuthResources.Errors.RefreshTokenRequired.Key, AuthResources.Errors.RefreshTokenRequired.Value));
                return CustomResponse();
            }

            return CustomResponse(await _authService.RevokeToken(token, IpAddress()));

        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken()
        {
            var jwtConfig = new Business.DTOs.JwtConfig(_appSettings.Secret, _appSettings.Issuer, _appSettings.ValidOn, _appSettings.ExpireMinutes);

            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _authService.RefreshToken(refreshToken, IpAddress(), jwtConfig);

            if (response == null)
            {
                NotifyError(new Notification(AuthResources.Errors.UserBlocked.Key, AuthResources.Errors.UserBlocked.Value));
            }
            else
                SetTokenCookie(response.RefreshToken);

            return CustomResponse(response);
        }

        private async Task<LoginResponseViewModel> GenerateJwt(string userName)
        {
            var user = await _userService.GetByUserName(userName);
            var claims = await _userService.GetClaims(user);
            var jwtConfig = new Business.DTOs.JwtConfig(_appSettings.Secret, _appSettings.Issuer, _appSettings.ValidOn, _appSettings.ExpireMinutes);

            var encodedToken = await _authService.GenerateJwtToken(user.Id, claims, jwtConfig);
            var refreshToken = await _authService.CreateRefreshToken(user.Id, IpAddress(), _appSettings.ExpireMinutes);

            SetTokenCookie(refreshToken);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromMinutes(_appSettings.ExpireMinutes).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    Name = user.Name,
                    Claims = claims.Select(c => new PermissionViewModel { Type = c.Type, Value = c.Value })
                }
            };
            return response;
        }
        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                SameSite = SameSiteMode.None,
                Secure = true,
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.RefreshTokenExpireMinutes)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}