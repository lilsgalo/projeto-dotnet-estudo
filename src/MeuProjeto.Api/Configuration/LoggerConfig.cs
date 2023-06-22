using System;
using Microsoft.Extensions.Configuration;
using System.Text;
using MeuProjeto.Api.Extensions;
using NLog;
using NLog.Web.LayoutRenderers;
using System.Linq;
using NLog.LayoutRenderers;
using Microsoft.Extensions.DependencyInjection;

namespace MeuProjeto.Api.Configuration
{
    public class UserIdLayoutRenderer : AspNetLayoutRendererBase
    {
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            var id = HttpContextAccessor.HttpContext.User.GetUserId();
            builder.Append(id == Guid.Empty ? "" : id.ToString());
        }
    }

    public class HeadersLayoutRenderer : AspNetLayoutRendererBase
    {
        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            var headers = HttpContextAccessor.HttpContext.Request.Headers;
            var values = headers.Values.AsEnumerable();
            var list = headers.Keys.Select((k, i) => k + ": " + values.ElementAt(i));
            builder.Append(String.Join("\n", list));
        }
    }

    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection service, IConfiguration configuration)
        {
            LayoutRenderer.Register<UserIdLayoutRenderer>("userid");
            LayoutRenderer.Register<HeadersLayoutRenderer>("headers");


            service.AddHealthChecks()
                .AddCheck<ApiHealthCheck>("Api")
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "SQL Server");
                //.AddNpgSql(configuration.GetConnectionString("DefaultConnection"), name: "PostgreSQL");

            service
                .AddHealthChecksUI(setup =>
                {
                    setup.DisableDatabaseMigrations();
                    setup.SetEvaluationTimeInSeconds(10);
                })
                .AddInMemoryStorage();
            //AddPostgreSqlStorage(configuration.GetConnectionString("DefaultConnection"));

            return service;
        }

        /* public static IApplicationBuilder UseLoggingConfiguration (this IApplicationBuilder app)
         {

             app.UseHealthChecks(path: "/api/hc", new HealthCheckOptions()
             {
                 Predicate = _ => true,
                 ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
             });

             app.UseHealthChecksUI(options => { options.UIPath = "/api/hc-ui"; });

             return app;
         }*/


    }
}