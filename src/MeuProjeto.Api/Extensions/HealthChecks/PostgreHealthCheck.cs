//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Diagnostics.HealthChecks;
//using Npgsql;

//namespace MeuProjeto.Api.Extensions
//{
//    public class PostgreHealthCheck : IHealthCheck
//    {
//        readonly string _connection;

//        public PostgreHealthCheck(string connection)
//        {
//            _connection = connection;
//        }

//        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
//        {
//            try
//            {
//                using (var connection = new NpgsqlConnection(_connection))
//                {
//                    await connection.OpenAsync(cancellationToken);

//                    var command = connection.CreateCommand();
//                    command.CommandText = "select count(*) from Asp_Net_Users";
                    
//                    return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0 ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
//                }
//            }
//            catch (Exception)
//            {
//                return HealthCheckResult.Unhealthy();
//            }
//        }
//    }
//}