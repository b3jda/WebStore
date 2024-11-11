using WebStore.Models;

namespace WebStore.Services.Interfaces
{
    public interface IBrandService
    {
        Task<Brand> GetBrandById(int brandId);
        Task<IEnumerable<Brand>> GetAllBrands();
        Task AddBrand(Brand brand);
        Task UpdateBrand(Brand brand, int brandId);
        Task DeleteBrand(int brandId);
    }
}
