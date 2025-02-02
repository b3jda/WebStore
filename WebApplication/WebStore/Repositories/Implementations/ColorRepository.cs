using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Models;
using WebStore.Repositories.Interfaces;
namespace WebStore.Repositories.Implementations
{
    public class ColorRepository : IColorRepository
    {
        private readonly AppDbContext _context;

        public ColorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Color> GetColorById(int colorId)
        {
            return await _context.Colors.FindAsync(colorId);
        }

        public async Task<IEnumerable<Color>> GetAllColors()
        {
            return await _context.Colors.ToListAsync();
        }

        public async Task AddColor(Color color)
        {
            _context.Colors.Add(color);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateColor(Color color, int colorId)
        {
            var existingColor = await _context.Colors.FindAsync(colorId);
            if (existingColor != null)
            {
                existingColor.Name = color.Name;
                _context.Colors.Update(existingColor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteColor(int colorId)
        {
            var existingColor = await _context.Colors.FindAsync(colorId);
            if (existingColor != null)
            {
                _context.Colors.Remove(existingColor);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Color> GetColorByName(string name)
        {
            return await _context.Colors.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
