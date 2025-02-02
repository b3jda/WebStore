using WebStore.Models;
using WebStore.Repositories.Interfaces;
using WebStore.Services.Interfaces;

namespace WebStore.Services.Implementations
{
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository _genderRepository;

        public GenderService(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<Gender> GetGenderById(int genderId)
        {
            return await _genderRepository.GetGenderById(genderId);
        }

        public async Task<IEnumerable<Gender>> GetAllGenders()
        {
            return await _genderRepository.GetAllGenders();
        }

        public async Task AddGender(Gender gender)
        {
            await _genderRepository.AddGender(gender);
        }

        public async Task UpdateGender(Gender gender, int genderId)
        {
            await _genderRepository.UpdateGender(gender, genderId);
        }

        public async Task DeleteGender(int genderId)
        {
            await _genderRepository.DeleteGender(genderId);
        }
    }
}

