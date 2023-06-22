using AutoMapper;
using MeuProjeto.Api.Configuration;
using MeuProjeto.Api.Extensions;
using MeuProjeto.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Text;

namespace MeuProjeto.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostEnvironment CurrentEnviroment { get; private set; }

        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            CurrentEnviroment = hostEnvironment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MeuDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            //use postgreSQL
            //services.AddDbContext<MeuDbContext>(options =>
            //{
            //    options
            //    .UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            //    .UseSnakeCaseNamingConvention();
            //});

            services.AddIdentityConfig(Configuration);

            services.AddApiConfig(Configuration);

            services.AddSwaggerConfig();

            services.AddLoggingConfig(Configuration);

            services.ResolveDependencies();

            #if DEBUG
            //Seed Data on Debug
            services.AddHostedService<SeedConfig>();

            //Disable Authorization on Debug
            services.AddSingleton<IAuthorizationHandler, AllowAnonymous>();
            #endif
        }

        public void Configure(WebApplication app, IWebHostEnvironment env/*, IApiVersionDescriptionProvider provider*/)
        {
            app.UseApiConfig(env);

            app.UseSwaggerConfig(/*provider,*/ Configuration);
        }
    }
}
