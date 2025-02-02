using WebStore.DTOs;
using WebStore.Models;

namespace WebStore.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int productId);
        Task<IEnumerable<Product>> GetAllProducts();
        Task AddProduct(Product product);
        Task UpdateProduct(Product product, int productId);
        Task DeleteProduct(int productId);
        Task ApplyDiscount(int productId, double discountPercentage);
        Task RemoveDiscount(int productId);
        Task<IEnumerable<Product>> SearchProducts(
            string category = null,
            string gender = null,
            string brand = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string size = null,
            string color = null,
            bool? inStock = null);
        Task<ProductStockDTO> UpdateStock(int productId, int quantitySold);
        Task<ProductStockDTO> GetRealTimeProductStock(int productId);
        Task<List<Product>> GetDiscountedProductsAsync();

    }
}
