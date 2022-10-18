using BookStore.DL.Interfaces;
using BookStore.Models.Configurations;
using BookStore.Models.Models.Users;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookStore.DL.Repositories.Mongo
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly IOptions<MongoDbConfiguration> _options;
        private readonly IMongoClient _dbClient;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Purchase> _purchaseCollection;

        public PurchaseRepository(IOptions<MongoDbConfiguration> options)
        {
            _options = options;

            _dbClient = new MongoClient(_options.Value.ConnectionString);

            _database = _dbClient.GetDatabase(_options.Value.DatabaseName);
            _purchaseCollection = _database.GetCollection<Purchase>("Purchase");
        }

        public async Task<IEnumerable<Purchase>> GetPurchase(int userId)
        {            
            var filter = Builders<Purchase>.Filter.Eq("UserId", userId);

            var doc = _purchaseCollection.Find(filter).ToList();

            return doc;
        }

        public Task<Purchase> UpdatePurchase(Purchase purchase)
        {
            var filter = Builders<Purchase>.Filter.Eq("Id", purchase.Id);
            var update = Builders<Purchase>.Update.Set("Books", purchase.Books);

            _purchaseCollection.UpdateOne(filter, update);

            return Task.FromResult(purchase);
        }

        public Task<Purchase?> SavePurchase(Purchase? purchase)
        {
            var document = new Purchase()
            {
                Books = purchase.Books,
                TotalMoney = purchase.TotalMoney,
                UserId = purchase.UserId
            };

            _purchaseCollection.InsertOne(document);

            return Task.FromResult(purchase);
        }

        public Task<Guid> DeletePurchase(Guid purchaseId)
        {
            _purchaseCollection.DeleteOne(x => x.Id == purchaseId);

            return Task.FromResult(purchaseId);
        }
    }
}
