using WebStore.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebStore.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDTO> GetProductById(int productId);
        Task<IEnumerable<ProductResponseDTO>> GetAllProducts();
        Task<ProductResponseDTO> AddProduct(ProductRequestDTO productRequest);
        Task UpdateProduct(ProductRequestDTO productRequest, int productId);
        Task DeleteProduct(int productId);
        Task ApplyDiscount(int productId, double discountPercentage);
        Task<IEnumerable<ProductResponseDTO>> SearchProducts(
            string category = null,
            string gender = null,
            string brand = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string size = null,
            string color = null,
            bool? inStock = null);
        Task<ProductStockDTO> GetRealTimeProductStock(int productId);
    }
}
