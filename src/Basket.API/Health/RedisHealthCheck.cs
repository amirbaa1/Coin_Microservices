using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Basket.API.Health
{
    public class RedisHealthCheck : IHealthCheck
    {
        private readonly IDistributedCache _distributedCache;
        private readonly string _connectionString;

        public RedisHealthCheck(IDistributedCache distributedCache, string connectionString)
        {
            _distributedCache = distributedCache;
            _connectionString = connectionString;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                _distributedCache.GetStringAsync("health-check-key", cancellationToken).GetAwaiter().GetResult();
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception ex)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy($"Failed to connect to Redis: {ex.Message}"));
            }
        }
    }
}
