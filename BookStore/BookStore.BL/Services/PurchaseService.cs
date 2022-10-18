using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models.Users;
using Microsoft.Extensions.Logging;

namespace BookStore.BL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ILogger<PurchaseService> _logger;
        private readonly IPurchaseRepository _purchaseRepository;
        public PurchaseService(ILogger<PurchaseService> logger, IPurchaseRepository purchaseRepository)
        {
            _logger = logger;
            _purchaseRepository = purchaseRepository;
        }

        public async Task<Guid> DeletePurchase(Guid purchaseId)
        {
            return await _purchaseRepository.DeletePurchase(purchaseId);
        }

        public async Task<IEnumerable<Purchase>> GetPurchase(int userId)
        {
            return await _purchaseRepository.GetPurchase(userId);
        }

        public async Task<Purchase?> SavePurchase(Purchase purchase)
        {
            return await _purchaseRepository.SavePurchase(purchase);
        }

        public async Task<Purchase> UpdatePurchase(Purchase purchase)
        {
            return await _purchaseRepository.UpdatePurchase(purchase);
        }
    }
}
