using BookStore.DL.Interfaces;
using BookStore.DL.Repositories.Mongo;
using BookStore.Models.Configurations;
using BookStore.Models.Models.Users;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks.Dataflow;

namespace BookStore.BL.Kafka
{
    public class PurchaseConsumer : ConsumerHostedService<int, Purchase>
    {
        private readonly ILogger<PurchaseConsumer> _logger;
        private readonly IOptions<KafkaSettings> _kafkaSettings;
        private readonly IBookRepository _bookRepository;
        //private readonly PurchaseRepository _purchase;
        private TransformBlock<Purchase, Purchase> transformerBlock;

        public PurchaseConsumer(IOptions<KafkaSettings> kafkaSettings, ILogger<PurchaseConsumer> logger,
                                IBookRepository bookRepository)
                        : base(kafkaSettings, logger)
        {
            _kafkaSettings = kafkaSettings;
            _logger = logger;
            _bookRepository = bookRepository;

            transformerBlock = new TransformBlock<Purchase, Purchase>( p =>
            {
                foreach (var b in p.Books)
                {
                    if (b != null)
                    {
                        b.Quantity++;
                    }    
                }
                return p;
            });

            var actionBlock = new ActionBlock<Purchase>(p =>
            {
                transformerBlock.Post(p);
                Console.WriteLine($"Received : {p.UserId} --> {p.Books.Count()}");
            });
            transformerBlock.LinkTo(actionBlock);
        }

        public override void HandleMessage(Purchase value)
        {
            transformerBlock.SendAsync(value);
        }
    }
}
