using BookStore.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Kafka
{
    public abstract class ConsumerHostedService<TKey, TValue> : IHostedService
    {
        private readonly ILogger<ConsumerHostedService<TKey, TValue>> _logger;
        private readonly IOptions<KafkaSettings> _kafkaSettings;
        private readonly string _topicName;
        private readonly ConsumerConfig _consumerConfig;
        private readonly IConsumer<TKey, TValue> _consumerBuilder;

        public ConsumerHostedService(IOptions<KafkaSettings> kafkaSettings, ILogger<ConsumerHostedService<TKey, TValue>> logger)
        {
            _kafkaSettings = kafkaSettings;
            _topicName = typeof(TValue).Name;
            _logger = logger;
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

        public abstract void HandleMessage(TValue value);


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start -> {nameof(ConsumerHostedService<TKey, TValue>)}");

            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var cr = _consumerBuilder.Consume();

                    if (cr != null)
                        HandleMessage(cr.Message.Value);
                }

            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}