using BookStore.BL.Kafka;
using BookStore.Models.Configurations;
using BookStore.Models.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookStore.Caches.Cache
{
    public class CacheService<TKey, TValue> where TValue : ICacheItem<TKey>
    {
        private readonly ILogger<CacheService<TKey, TValue>> _logger;
        private IOptions<KafkaSettings> _kafkaSettings;
        private readonly string _topicName;
        private readonly ConsumerConfig _consumerConfig;
        private readonly IConsumer<TKey, TValue> _consumerBuilder;
        private readonly IDictionary<TKey, TValue> _cacheDict;

        public CacheService(ILogger<CacheService<TKey, TValue>> logger, IOptions<KafkaSettings> kafkaSettings)
        {
            _logger = logger;
            _kafkaSettings = kafkaSettings;
            _topicName = typeof(TValue).Name;
            _cacheDict = new Dictionary<TKey, TValue>();

            _consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = _kafkaSettings.Value.BootstrapServers,
                AutoOffsetReset = (AutoOffsetReset?)_kafkaSettings.Value.AutoOffsetReset,
                GroupId = _kafkaSettings.Value.GroupId
            };

            _consumerBuilder = new ConsumerBuilder<TKey, TValue>(_consumerConfig)
                .SetKeyDeserializer(new MsgPackDeserializer<TKey>())
                .SetValueDeserializer(new MsgPackDeserializer<TValue>())
                .Build();
            _consumerBuilder.Subscribe(_topicName);
        }

        public async Task GetCache(CancellationToken cancellationToken)
        {
            Task.Run( async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var cr = _consumerBuilder.Consume();
                    _cacheDict.Add(cr.Message.Key, cr.Message.Value);

                    _logger.LogInformation($"Delivered item! {cr.Message.Key} --> {cr.Message.Value}");
                }

            }, cancellationToken);

            //return Task.CompletedTask;
        }
    }
}
