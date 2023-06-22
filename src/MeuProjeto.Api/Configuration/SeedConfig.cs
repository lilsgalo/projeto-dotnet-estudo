using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Infrastructure.Data.Context;
using MeuProjeto.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MeuProjeto.Api.Configuration
{
    public class SeedConfig : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public SeedConfig(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }      

        public async Task StartAsync(CancellationToken cancellationToken)
        {         
            using (var scope = _serviceProvider.CreateScope())
            {                
                var meuDbContext = scope.ServiceProvider.GetRequiredService<MeuDbContext>();

                if (await meuDbContext.AllMigrationsApplied())
                {
                    await meuDbContext.EnsureSeeded(scope);
                }                
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
       
}
