using WebStore.Models;

namespace WebStore.Repositories.Interfaces
{
    public interface IDiscountRepository
    {
        Task<Discount> GetDiscountById(int discountId);
        Task<IEnumerable<Discount>> GetAllDiscounts();
        Task<Discount> AddDiscount(Discount discount);
        Task UpdateDiscount(Discount discount, int discountId);
        Task DeleteDiscount(int discountId);
        Task<Discount> GetDiscountForProduct(int productId);
    }
}
