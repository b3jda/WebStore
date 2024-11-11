using WebStore.Models;

namespace WebStore.Services.Interfaces
{
    public interface IGenderService
    {
        Task<Gender> GetGenderById(int genderId);
        Task<IEnumerable<Gender>> GetAllGenders();
        Task AddGender(Gender gender);
        Task UpdateGender(Gender gender, int genderId);
        Task DeleteGender(int genderId);
    }
}
