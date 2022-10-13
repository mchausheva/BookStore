using BookStore.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Kafka
{
    public class ProducerService<TKey, TValue>
    {
        private IOptions<KafkaSettings> _kafkaSettings;
        private readonly string _topicName;
        public ProducerService(IOptions<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
            _topicName = "test";
        }

        public void SendMessage(TKey key, TValue value)
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = _kafkaSettings.Value.BootstrapServers
            };

            var producer = new ProducerBuilder<TKey, TValue>(config)
                            .SetKeySerializer(new MsgPackSerializer<TKey>())
                            .SetValueSerializer(new MsgPackSerializer<TValue>())
                            .Build();
            try
            {
                var msg = new Message<TKey, TValue>()
                {
                    Key = key,
                    Value = value
                };

                var result = producer.ProduceAsync(_topicName, msg);
                if (result != null)
                    Console.WriteLine($"Delivered");
            }
            catch (ProduceException<int, int> ex)
            {
                Console.WriteLine($"Delivery faild: {ex.Error.Reason}");
            }
        }
    }
}
