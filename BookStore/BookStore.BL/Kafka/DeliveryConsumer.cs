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
                var b = _bookRepository.GetById(del.Book.Id).Result;

                if (b != null)
                {
                    b.Quantity--;
                }

                del.Book = _bookRepository.UpdateBook(b).Result;
                return del;
            });

            var actionBlock = new ActionBlock<Delivery>(del =>
            { 
                transformerBlock.Post(del);
            });

            transformerBlock.LinkTo(actionBlock);
        }

        public override void HandleMessage(Delivery value)
        {
            transformerBlock.SendAsync(value);
        }
    }
}
