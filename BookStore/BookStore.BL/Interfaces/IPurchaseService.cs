using BookStore.Models.Models.Users;

namespace BookStore.BL.Interfaces
{
    public interface IPurchaseService
    {
        Task<Purchase?> SavePurchase(Purchase purchase);
        Task<Guid> DeletePurchase(Guid purchaseId);
        Task<IEnumerable<Purchase>> GetPurchase(int userId);
        Task<Purchase> UpdatePurchase(Purchase purchase);
    }
}
