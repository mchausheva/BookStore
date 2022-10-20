using BookStore.BL.Provider;
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
        private readonly IAuthorRepository _authorRepository;

        private TransformBlock<Purchase, Purchase> transformerBlock;
        private ActionBlock<Purchase> actionBlock;

        private readonly HttpClientService _clientService;

        public PurchaseConsumer(IOptions<KafkaSettings> kafkaSettings, ILogger<PurchaseConsumer> logger,
                                IBookRepository bookRepository, IAuthorRepository authorRepository,
                                HttpClientService clientService)
                        : base(kafkaSettings, logger)
        {
            _kafkaSettings = kafkaSettings;
            _logger = logger;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _clientService = clientService;

            transformerBlock = new TransformBlock<Purchase, Purchase>(p =>
            {
                foreach (var b in p.Books)
                {
                    var book = _bookRepository.GetById(b.Id).Result;
                    if (book != null)
                    {
                        book.Quantity++;
                    }

                    var auth = _authorRepository.GetById(b.AuthorId).Result;
                    if (auth != null)
                    {
                        var info = _clientService.AdditionalInfo().Result;
                        p.AdditionalInfo.ToList().Add(info.AdditionalInfo);
                    }
                }     
                return p;
            });

            actionBlock = new ActionBlock<Purchase>(p =>
            {
                transformerBlock.Post(p);
                Task.Delay(2000);
                Console.WriteLine($"RECEIVED ---> {p.AdditionalInfo}");
            });
            transformerBlock.LinkTo(actionBlock);
        }

        public override void HandleMessage(Purchase value)
        {
            transformerBlock.SendAsync(value);
        }
    }
}
