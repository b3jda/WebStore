using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.DTOs;
using AutoMapper;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _genderService;
        private readonly IMapper _mapper;

        public GenderController(IGenderService genderService, IMapper mapper)
        {
            _genderService = genderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenderResponseDTO>>> GetAllGenders()
        {
            var genders = await _genderService.GetAllGenders();
            var genderDTOs = _mapper.Map<IEnumerable<GenderResponseDTO>>(genders);
            return Ok(genderDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenderResponseDTO>> GetGenderById(int id)
        {
            var gender = await _genderService.GetGenderById(id);
            if (gender == null)
                return NotFound();

            var genderDTO = _mapper.Map<GenderResponseDTO>(gender);
            return Ok(genderDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AddGender([FromBody] GenderRequestDTO genderRequest)
        {
            var gender = _mapper.Map<Gender>(genderRequest);
            await _genderService.AddGender(gender);
            var genderResponse = _mapper.Map<GenderResponseDTO>(gender);
            return CreatedAtAction(nameof(GetGenderById), new { id = gender.Id }, genderResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGender(int id, [FromBody] GenderRequestDTO genderRequest)
        {
            var gender = _mapper.Map<Gender>(genderRequest);
            await _genderService.UpdateGender(gender, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGender(int id)
        {
            await _genderService.DeleteGender(id);
            return NoContent();
        }
    }
}
