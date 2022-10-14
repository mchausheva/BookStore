using BookStore.Models.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookStore.Caches.Cache
{
    public class CacheConsumer<TKey, TValue> : IHostedService where TValue : ICacheItem<TKey>
    {
        private readonly ILogger<CacheConsumer<TKey, TValue>> _logger;
        private readonly CacheService<int, Book> _cacheService;

        public CacheConsumer(ILogger<CacheConsumer<TKey, TValue>> logger, CacheService<int, Book> cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start {nameof(StartAsync)} --> TValue : {typeof(TValue)}");

            _cacheService.GetCache(cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Stop {nameof(StartAsync)} --> TValue : {typeof(TValue)}");

            return Task.CompletedTask;
        }
    }
}
