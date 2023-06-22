using MeuProjeto.Api.Controllers;
using MeuProjeto.Api.Extensions;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Notifications;
using MeuProjeto.Business.Services;
using MeuProjeto.Infrastructure.Data.Context;
using MeuProjeto.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MeuProjeto.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<AboutController>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserManualRepository, UserManualRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ISettingsRepository, SettingsRepository>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ICustomLogger, CustomLogger>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IUserManualService, UserManualService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ISettingsService, SettingsService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddTransient<ITicketStore, InMemoryTicketStore>();
            services.AddSingleton<IPostConfigureOptions<CookieAuthenticationOptions>, ConfigureCookieAuthenticationOptions>();

            services.AddHostedService<LongRunningService>();

            services.AddSingleton<BackgroundWorkerQueue>();

            return services;
        }
    }
}