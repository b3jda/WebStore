using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Models;
using WebStore.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebStore.Repositories.Implementations
{
    public class GenderRepository : IGenderRepository
    {
        private readonly AppDbContext _context;

        public GenderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Gender> GetGenderById(int genderId)
        {
            return await _context.Genders.FindAsync(genderId);
        }

        public async Task<IEnumerable<Gender>> GetAllGenders()
        {
            return await _context.Genders.ToListAsync();
        }

        public async Task AddGender(Gender gender)
        {
            _context.Genders.Add(gender);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGender(Gender gender, int genderId)
        {
            var existingGender = await _context.Genders.FindAsync(genderId);
            if (existingGender != null)
            {
                existingGender.Name = gender.Name;
                _context.Genders.Update(existingGender);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteGender(int genderId)
        {
            var existingGender = await _context.Genders.FindAsync(genderId);
            if (existingGender != null)
            {
                _context.Genders.Remove(existingGender);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Gender> GetGenderByName(string name)
        {
            return await _context.Genders.FirstOrDefaultAsync(g => g.Name == name);
        }
    }
}
