using BookStore.Models.Models.Users;

namespace BookStore.BL.Interfaces
{
    public interface IShoppingCartService
    {
        public Task<ShoppingCart> GetContent(int userId);
        public Task<ShoppingCart> AddToCart(ShoppingCart? shoppingCart);
        public Task RemoveFromCart(int userId, int itemId);
        public Task EmptyCart(Guid guidId);
        public Task FinishPurchase(int userId);
    }
}
