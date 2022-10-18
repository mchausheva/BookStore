using BookStore.DL.Interfaces;
using BookStore.Models.Configurations;
using BookStore.Models.Models.Users;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStore.DL.Repositories.Mongo
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IOptions<MongoDbConfiguration> _options;
        private readonly IMongoClient _dbClient;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<ShoppingCart> _carts;

        public ShoppingCartRepository(IOptions<MongoDbConfiguration> options)
        {
            _options = options;

            _dbClient = new MongoClient(_options.Value.ConnectionString);
            _database = _dbClient.GetDatabase(_options.Value.DatabaseName);
            _carts = _database.GetCollection<ShoppingCart>("ShoppingCart");
        }

        public Task<ShoppingCart?> Add(ShoppingCart? shoppingCart)
        {
            var document = new ShoppingCart()
            {
                Books = shoppingCart.Books,
                UserId = shoppingCart.UserId
            };

            _carts.InsertOne(document);

            return Task.FromResult(document);
        }

        public Task<Guid> Delete(Guid guidId)
        {
            _carts.DeleteOne(x => x.Id == guidId);

            return Task.FromResult(guidId);
        }

        public async Task<ShoppingCart?> Get(int userId)
        {
            var item = await _carts.FindAsync(x => x.UserId == userId);

            return item.FirstOrDefault();
        }
    }
}
