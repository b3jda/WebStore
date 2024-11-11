using WebStore.Models;

namespace WebStore.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        Task<Brand>GetBrandById(int brandId);
        Task<IEnumerable<Brand>> GetAllBrands();
        Task AddBrand(Brand brand);
        Task UpdateBrand(Brand brand, int brandId);
        Task DeleteBrand(int brandId);
        Task<Brand> GetBrandByName(string name);
    }
}
