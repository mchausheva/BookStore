using BookStore.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Kafka
{
    public class ConsumerHostedService<TKey, TValue> : IHostedService
    {
        ILogger<ConsumerHostedService<TKey, TValue>> _logger;
        private IOptions<KafkaSettings> _kafkaSettings;
        private readonly string _topicName;
        private readonly ConsumerConfig _consumerConfig;    
        public ConsumerHostedService(IOptions<KafkaSettings> kafkaSettings, ILogger<ConsumerHostedService<TKey, TValue>> logger)
        {
            _kafkaSettings = kafkaSettings;
            _topicName = "test";
            _logger = logger;
            _consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = _kafkaSettings.Value.BootstrapServers,
                AutoOffsetReset = (AutoOffsetReset?)_kafkaSettings.Value.AutoOffsetReset,
                GroupId = _kafkaSettings.Value.GroupId
            };
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _logger.LogInformation($"Start -> {nameof(ConsumerHostedService<TKey, TValue>)}");
               
            var consumer = new ConsumerBuilder<TKey, TValue>(_consumerConfig)
                .SetKeyDeserializer(new MsgPackDeserializer<TKey>())
                .SetValueDeserializer(new MsgPackDeserializer<TValue>())
                .Build();

            consumer.Subscribe(_topicName);

            var t = Task.Run(() =>
            {
                while (true)
                {
                    var cr = consumer.Consume();

                    Console.WriteLine($"Received msg with Key: [{cr.Message.Key}] & Value: {cr.Message.Value}");
                }
            }, cancellationToken);

            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Stopping -> {nameof(ConsumerHostedService<TKey, TValue>)}");

            return Task.CompletedTask;
        }
    }
}
