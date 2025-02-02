using WebStore.Data;
using WebStore.Models;
using WebStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace WebStore.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category, int categoryId)
        {
            var existingCategory = await _context.Categories.FindAsync(categoryId);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                _context.Categories.Update(existingCategory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCategory(int categoryId)
        {
            var existingCategory = await _context.Categories.FindAsync(categoryId);
            if (existingCategory != null)
            {
                _context.Categories.Remove(existingCategory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
