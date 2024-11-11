using WebStore.Models;

namespace WebStore.Repositories.Interfaces
{
    public interface IGenderRepository
    {
        Task<Gender> GetGenderById(int genderId);
        Task<IEnumerable<Gender>> GetAllGenders();
        Task AddGender(Gender gender);
        Task UpdateGender(Gender gender, int genderId);
        Task DeleteGender(int genderId);
        Task<Gender> GetGenderByName(string name);
    }
}
