using WebStore.Models;
using WebStore.Repositories.Interfaces;
using WebStore.Services.Interfaces;

namespace WebStore.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<Brand> GetBrandById(int brandId)
        {
            return await _brandRepository.GetBrandById(brandId);
        }

        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            return await _brandRepository.GetAllBrands();
        }

        public async Task AddBrand(Brand brand)
        {
            await _brandRepository.AddBrand(brand);
        }

        public async Task UpdateBrand(Brand brand, int brandId)
        {
            await _brandRepository.UpdateBrand(brand, brandId);
        }

        public async Task DeleteBrand(int brandId)
        {
            await _brandRepository.DeleteBrand(brandId);
        }
    }
}
