using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Models;
using WebStore.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebStore.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users
                .Include(u => u.Orders) 
                .ToListAsync();
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

    }

}
