using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MeuProjeto.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MeuProjeto.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, /*IApiVersionDescriptionProvider provider,*/ IConfiguration configuration)
        {
            var routePrefix = configuration.GetValue<string>("RoutePrefix");
            //app.UseMiddleware<SwaggerAuthorizedMiddleware>();
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/swagger/{documentname}/swagger.json";
                options.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{routePrefix}" } };
                    //swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"https://{httpReq.Host.Value}{routePrefix}" } };
                });
            });
            app.UseSwaggerUI(
                options =>
                {
                    //foreach (var description in provider.ApiVersionDescriptions)
                    //{
                    //options.SwaggerEndpoint($"{routePrefix}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    //}
                    var version = "v1";
                    options.SwaggerEndpoint($"{routePrefix}/swagger/{version}/swagger.json", version.ToUpperInvariant());
                });
            return app;
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        //readonly IApiVersionDescriptionProvider provider;
        private readonly IConfiguration _configuration;

        public ConfigureSwaggerOptions(/*IApiVersionDescriptionProvider provider,*/ IConfiguration configuration)
        {
            //this.provider = provider;
            this._configuration = configuration;
        }

        public void Configure(SwaggerGenOptions options)
        {
            //foreach (var description in provider.ApiVersionDescriptions)
            //{
            //    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, _configuration));
            //}
            options.SwaggerDoc("v1", CreateInfoForApiVersion(_configuration));
        }

        static OpenApiInfo CreateInfoForApiVersion(IConfiguration configuration)
        {
            var info = new OpenApiInfo()
            {
                Title = "API - Gestão de Ideias e Projetos",
                Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                Contact = new OpenApiContact() { Name = "CEPEDI", Email = "equipedotnet@cepedi.org.br" },
                License = new OpenApiLicense
                {
                    Name = "Health Ckecks",
                    Url = new Uri(configuration.GetSection("AppSettings").Get<AppSettings>().UrlBase + "/api/hc-ui"),
                }
            };

            return info;
        }
    }

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters)
            {
                var description = context.ApiDescription
                    .ParameterDescriptions
                    .First(p => p.Name == parameter.Name);

                var routeInfo = description.RouteInfo;

                operation.Deprecated = OpenApiOperation.DeprecatedDefault;

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (routeInfo == null)
                {
                    continue;
                }

                if (parameter.In != ParameterLocation.Path && parameter.Schema.Default == null)
                {
                    parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue.ToString());
                }

                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }

    public class SwaggerAuthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        public SwaggerAuthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger")
                && !context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next.Invoke(context);
        }
    }
}