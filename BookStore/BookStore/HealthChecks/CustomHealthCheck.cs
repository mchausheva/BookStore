using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BookStore.HealthChecks
{
    public class CustomHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message);
            }
            return HealthCheckResult.Healthy("OK");
        }
    }
}
