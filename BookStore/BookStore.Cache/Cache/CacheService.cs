using BookStore.Models.Models;
using Microsoft.Extensions.Logging;

namespace BookStore.Caches.Cache
{
    public class CacheService<TKey, TValue> where TValue : ICacheItem<TKey>
    {
        private readonly ILogger<CacheService<TKey, TValue>> _logger;
        private readonly IDictionary<TKey, TValue> _cacheDict;
        private readonly CacheConsumer<TKey, TValue> _cacheConsumer;
        private readonly CancellationTokenSource _token;

        public CacheService(ILogger<CacheService<TKey, TValue>> logger, CacheConsumer<TKey, TValue> cacheConsumer)
        {
            _logger = logger;
            _cacheDict = new Dictionary<TKey, TValue>();
            _token = new CancellationTokenSource();

            _cacheConsumer = cacheConsumer;
            _cacheConsumer.StartAsync(_cacheDict, _token.Token);
        }

        public async Task<IDictionary<TKey, TValue>> GetCacheDict()
        {
            _logger.LogInformation("Return dictionary with cache.");
            return await Task.FromResult(_cacheDict);
        }
    }
}
