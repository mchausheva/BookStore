using BookStore.Models.Models.Users;

namespace BookStore.DL.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<Purchase?> SavePurchase(Purchase? purchase);
        Task<Guid> DeletePurchase(Guid purchaseId);
        Task<IEnumerable<Purchase>> GetPurchase(int userId);
        Task<Purchase> UpdatePurchase(Purchase purchase);
    }
}
