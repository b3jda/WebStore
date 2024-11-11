using WebStore.Models;
using WebStore.Repositories.Interfaces;
using WebStore.Services.Interfaces;

namespace WebStore.Services.Implementations
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }

        public async Task<Size> GetSizeById(int sizeId)
        {
            return await _sizeRepository.GetSizeById(sizeId);
        }

        public async Task<IEnumerable<Size>> GetAllSizes()
        {
            return await _sizeRepository.GetAllSizes();
        }

        public async Task AddSize(Size size)
        {
            await _sizeRepository.AddSize(size);
        }

        public async Task UpdateSize(Size size, int sizeId)
        {
            await _sizeRepository.UpdateSize(size, sizeId);
        }

        public async Task DeleteSize(int sizeId)
        {
            await _sizeRepository.DeleteSize(sizeId);
        }
    }
}
