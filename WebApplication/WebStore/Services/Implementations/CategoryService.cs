using WebStore.Models;
using WebStore.Repositories.Interfaces;
using WebStore.Services.Interfaces;

namespace WebStore.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _categoryRepository.GetCategoryById(categoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _categoryRepository.GetAllCategories();
        }

        public async Task AddCategory(Category category)
        {
            await _categoryRepository.AddCategory(category);
        }

        public async Task UpdateCategory(Category category, int categoryId)
        {
            await _categoryRepository.UpdateCategory(category, categoryId);
        }

        public async Task DeleteCategory(int categoryId)
        {
            await _categoryRepository.DeleteCategory(categoryId);
        }
    }
}
