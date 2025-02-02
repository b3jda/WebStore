using WebStore.Models;
using WebStore.Repositories.Interfaces;
using WebStore.Services.Interfaces;

namespace WebStore.Services.Implementations
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;

        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public async Task<Color> GetColorById(int colorId)
        {
            return await _colorRepository.GetColorById(colorId);
        }

        public async Task<IEnumerable<Color>> GetAllColors()
        {
            return await _colorRepository.GetAllColors();
        }

        public async Task AddColor(Color color)
        {
            await _colorRepository.AddColor(color);
        }

        public async Task UpdateColor(Color color, int colorId)
        {
            await _colorRepository.UpdateColor(color, colorId);
        }

        public async Task DeleteColor(int colorId)
        {
            await _colorRepository.DeleteColor(colorId);
        }
    }
}
