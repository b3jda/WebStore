using WebStore.Models;

namespace WebStore.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryById(int categoryId);
        Task<IEnumerable<Category>> GetAllCategories();
        Task AddCategory(Category category);
        Task UpdateCategory(Category category, int categoryId);
        Task DeleteCategory(int categoryId);
        Task<Category> GetCategoryByName(string name);

    }
}
