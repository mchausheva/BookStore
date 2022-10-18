using BookStore.Models.Models.Users;

namespace BookStore.DL.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart?> Get(int userId);
        Task<ShoppingCart?> Add(ShoppingCart? shoppingCart);
        Task<Guid> Delete(Guid guidId);
    }
}
