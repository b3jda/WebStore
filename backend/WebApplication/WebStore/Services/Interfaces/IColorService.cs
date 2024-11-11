using WebStore.Models;

namespace WebStore.Services.Interfaces
{
    public interface IColorService
    {
        Task<Color> GetColorById(int colorId);
        Task<IEnumerable<Color>> GetAllColors();
        Task AddColor(Color color);
        Task UpdateColor(Color color, int colorId);
        Task DeleteColor(int colorId);
    }
}
