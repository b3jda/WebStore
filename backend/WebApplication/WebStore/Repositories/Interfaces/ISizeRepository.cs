using WebStore.Models;
namespace WebStore.Repositories.Interfaces
{
    public interface ISizeRepository
    {
        Task<Size> GetSizeById(int sizeId);
        Task<IEnumerable<Size>> GetAllSizes();
        Task AddSize(Size size);
        Task UpdateSize(Size size, int sizeId);
        Task DeleteSize(int sizeId);
        Task<Size> GetSizeByName(string name);
    }
}
