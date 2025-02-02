using WebStore.Models;

namespace WebStore.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<Discount> GetDiscountById(int discountId);
        Task<IEnumerable<Discount>> GetAllDiscounts();
        Task AddDiscount(Discount discount);
        Task UpdateDiscount(Discount discount, int discountId);
        Task DeleteDiscount(int discountId);
        Task<Discount> GetDiscountForProduct(int productId);
    }
}
