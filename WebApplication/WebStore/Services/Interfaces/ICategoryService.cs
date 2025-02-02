using WebStore.Models;

namespace WebStore.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryById(int categoryId);
        Task<IEnumerable<Category>> GetAllCategories();
        Task AddCategory(Category category);
        Task UpdateCategory(Category category, int categoryId);
        Task DeleteCategory(int categoryId);
    }
}
