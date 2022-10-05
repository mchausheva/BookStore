using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data.SqlClient;

namespace BookStore.HealthChecks
{
    public class SqlHealthCkeck : IHealthCheck
    {
        private readonly IConfiguration _configuration;
        public SqlHealthCkeck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                }
                catch (SqlException ex)
                {
                    return HealthCheckResult.Unhealthy(ex.Message);
                }
                return HealthCheckResult.Healthy("SQL connection is OK");
            }
        }
    }
}
