using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Models;
using WebStore.Repositories.Interfaces;

namespace WebStore.Repositories.Implementations
{
    public class SizeRepository : ISizeRepository
    {
        private readonly AppDbContext _context;

        public SizeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Size> GetSizeById(int sizeId)
        {
            return await _context.Sizes.FindAsync(sizeId);
        }

        public async Task<IEnumerable<Size>> GetAllSizes()
        {
            return await _context.Sizes.ToListAsync();
        }

        public async Task AddSize(Size size)
        {
            _context.Sizes.Add(size);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSize(Size size, int sizeId)
        {
            var existingSize = await _context.Sizes.FindAsync(sizeId);
            if (existingSize != null)
            {
                existingSize.Name = size.Name;
                _context.Sizes.Update(existingSize);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteSize(int sizeId)
        {
            var existingSize = await _context.Sizes.FindAsync(sizeId);
            if (existingSize != null)
            {
                _context.Sizes.Remove(existingSize);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Size> GetSizeByName(string name)
        {
            return await _context.Sizes.FirstOrDefaultAsync(s => s.Name == name);
        }
    }
}
