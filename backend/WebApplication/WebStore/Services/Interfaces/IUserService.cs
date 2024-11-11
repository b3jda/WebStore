using WebStore.DTOs;
using WebStore.Models;

namespace WebStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> AddUser(UserResponseDTO userDto);
        Task<UserResponseDTO> GetUserById(string userId);
        Task<IEnumerable<UserResponseDTO>> GetAllUsers();
        Task<bool> DeleteUser(string userId);

    }
}
