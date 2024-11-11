using WebStore.Models;

namespace WebStore.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUserById(string userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task DeleteUser(string userId);

    }
}
