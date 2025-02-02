using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Repositories.Interfaces;
using WebStore.Services.Interfaces;

namespace WebStore.Services.Implementations
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<Discount> GetDiscountById(int discountId)
        {
            return await _discountRepository.GetDiscountById(discountId);
        }

        public async Task<IEnumerable<Discount>> GetAllDiscounts()
        {
            return await _discountRepository.GetAllDiscounts();
        }

        public async Task AddDiscount(Discount discount)
        {
            await _discountRepository.AddDiscount(discount);
        }

        public async Task UpdateDiscount(Discount discount, int discountId)
        {
            await _discountRepository.UpdateDiscount(discount, discountId);
        }

        public async Task DeleteDiscount(int discountId)
        {
            await _discountRepository.DeleteDiscount(discountId);
        }

        public async Task<Discount> GetDiscountForProduct(int productId)
        {
            return await _discountRepository.GetDiscountForProduct(productId);
        }
    }
}
