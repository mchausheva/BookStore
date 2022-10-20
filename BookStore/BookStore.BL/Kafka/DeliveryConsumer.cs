using BookStore.DL.Interfaces;
using BookStore.Models.Configurations;
using BookStore.Models.Models.Users;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks.Dataflow;

namespace BookStore.BL.Kafka
{
    public class DeliveryConsumer : ConsumerHostedService<int, Delivery>
    {
        private readonly ILogger<DeliveryConsumer> _logger;
        private readonly IOptions<KafkaSettings> _kafkaSettings;
        private readonly IBookRepository _bookRepository;
        private TransformBlock<Delivery, Delivery> transformerBlock;

        public DeliveryConsumer(IOptions<KafkaSettings> kafkaSettings, ILogger<DeliveryConsumer> logger,
                                IBookRepository bookRepository)
                        : base(kafkaSettings, logger)
        {
            _kafkaSettings = kafkaSettings;
            _logger = logger;
            _bookRepository = bookRepository;

            transformerBlock = new TransformBlock<Delivery, Delivery>(del =>
            {
                if (del.Book.Id != null)
                {
                    del.Book.Quantity--;
                }    
                return del;
            });

            var actionBlock = new ActionBlock<Delivery>(del =>
            { 
                transformerBlock.Post(del);
                Console.WriteLine($"Received : {del.Book.Title} --> {del.Book.Quantity}");
            });

            transformerBlock.LinkTo(actionBlock);
        }

        public override void HandleMessage(Delivery value)
        {
            transformerBlock.SendAsync(value);
        }
    }
}
