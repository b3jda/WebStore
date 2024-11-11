using WebStore.Data;
using WebStore.Models;
using WebStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace WebStore.Repositories.Implementations
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _context;

        public BrandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Brand> GetBrandById(int brandId)
        {
            return await _context.Brands.FindAsync(brandId);
        }

        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task AddBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBrand(Brand brand, int brandId)
        {
            var existingBrand = await _context.Brands.FindAsync(brandId);
            if (existingBrand != null)
            {
                existingBrand.Name = brand.Name;
                _context.Brands.Update(existingBrand);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBrand(int brandId)
        {
            var existingBrand = await _context.Brands.FindAsync(brandId);
            if (existingBrand != null)
            {
                _context.Brands.Remove(existingBrand);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Brand> GetBrandByName(string name)
        {
            return await _context.Brands.FirstOrDefaultAsync(b => b.Name == name);
        }
    }
}
