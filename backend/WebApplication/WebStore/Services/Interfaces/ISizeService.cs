using WebStore.Models;

namespace WebStore.Services.Interfaces
{
    public interface ISizeService
    {
        Task<Size> GetSizeById(int sizeId);
        Task<IEnumerable<Size>> GetAllSizes();
        Task AddSize(Size size);
        Task UpdateSize(Size size, int sizeId);
        Task DeleteSize(int sizeId);
    }
}
