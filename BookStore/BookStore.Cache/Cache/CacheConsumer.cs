using BookStore.BL.Kafka;
using BookStore.Models.Configurations;
using BookStore.Models.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookStore.Caches.Cache
{
    public class CacheConsumer<TKey, TValue> where TValue : ICacheItem<TKey>
    {
        private readonly ILogger<CacheConsumer<TKey, TValue>> _logger;
        private IOptions<KafkaSettings> _kafkaSettings;
        private readonly string _topicName;
        private readonly ConsumerConfig _consumerConfig;
        private readonly IConsumer<TKey, TValue> _consumerBuilder;

        public CacheConsumer(ILogger<CacheConsumer<TKey, TValue>> logger, IOptions<KafkaSettings> kafkaSettings)
        {
            _logger = logger;
            _kafkaSettings = kafkaSettings;
            _topicName = typeof(TValue).Name;

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

        public Task StartAsync(IDictionary<TKey, TValue> dict, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start {nameof(StartAsync)} --> TValue : {typeof(TValue)}");

            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var cr = _consumerBuilder.Consume();
                    
                    if (dict.ContainsKey(cr.Key))
                    {
                        dict[cr.Key] = cr.Value;
                    }
                    else
                    {
                        dict.Add(cr.Key, cr.Value);
                        _logger.LogInformation($"Delivered item! {cr.Key} --> {cr.Message.Value}");
                    }
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }
    }
}
