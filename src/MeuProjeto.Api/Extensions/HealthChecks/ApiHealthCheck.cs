using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using MeuProjeto.Api.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MeuProjeto.Api.Extensions
{
    public class ApiHealthCheck : IHealthCheck
    {
        private AboutController _aboutController;
        private IConfiguration _configuration;

        public ApiHealthCheck(AboutController aboutController, IConfiguration configuration)
        {
            this._aboutController = aboutController;
            this._configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("https://localhost:" + _configuration.GetSection("AppSettings").Get<AppSettings>().SSLPort);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/api/v1/about/version");
                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy();
                }


                return HealthCheckResult.Unhealthy();

            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}