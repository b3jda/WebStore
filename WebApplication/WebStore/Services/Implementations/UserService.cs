using AutoMapper;
using WebStore.DTOs;
using WebStore.Models;
using WebStore.Repositories.Interfaces;
using WebStore.Services.Interfaces;

namespace WebStore.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO> AddUser(UserResponseDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.AddUser(user);
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> GetUserById(string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
                return false;

            await _userRepository.DeleteUser(userId);
            return true;
        }

    }
}
