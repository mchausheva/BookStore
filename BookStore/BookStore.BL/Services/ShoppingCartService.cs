using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Users;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;

namespace BookStore.BL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ILogger<ShoppingCartService> _logger;

        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IPurchaseService _purchaseService;

        private readonly IMapper _mapper;

        public ShoppingCartService(ILogger<ShoppingCartService> logger, IShoppingCartRepository shoppingCartRepository, IPurchaseService purchaseService, IMapper mapper)
        {
            _logger = logger;
            _shoppingCartRepository = shoppingCartRepository;
            _purchaseService = purchaseService;
            _mapper = mapper;
        }

        public async Task<ShoppingCart> AddToCart(ShoppingCart? shoppingCart)
        {
            await _shoppingCartRepository.Add(shoppingCart);

            return shoppingCart;
        }

        public Task EmptyCart(Guid guidId)
        {
            _shoppingCartRepository.Delete(guidId);
            return Task.CompletedTask;
        }

        public async Task FinishPurchase(int userId)
        {
            var temp = await _shoppingCartRepository.Get(userId);

            var document = new Purchase()
            {
                UserId = userId,
                Books = temp.Books,
                TotalMoney = temp.Books.Sum(x => x.Price)
            };
            
            await _purchaseService.SavePurchase(document);
            await EmptyCart(temp.Id);
        }

        public Task<ShoppingCart> GetContent(int userId)
        {
            return _shoppingCartRepository.Get(userId);
        }

        public async Task RemoveFromCart(int userId, int itemId)
        {
            var cart = await GetContent(userId);

            if (cart != null && cart.Books.Any(x => x.Id == itemId))
            {
                var newCart = await _shoppingCartRepository.Add(new ShoppingCart()
                {
                    UserId = userId,
                    Books = null
                });
                await _shoppingCartRepository.Add(newCart);
            }
        }
    }
}
