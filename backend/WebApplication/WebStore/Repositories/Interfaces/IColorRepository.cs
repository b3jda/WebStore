using WebStore.Models;
namespace WebStore.Repositories.Interfaces
{
    public interface IColorRepository
    {
        Task<Color> GetColorById(int colorId);
        Task<IEnumerable<Color>> GetAllColors();
        Task AddColor(Color color);
        Task UpdateColor(Color color, int colorId);
        Task DeleteColor(int colorId);
        Task<Color> GetColorByName(string name);
    }
}
