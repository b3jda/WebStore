using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.DTOs;
using AutoMapper;
using WebStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebStore.Controllers
{
    /// <summary>
    /// Manages gender-related operations.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/gender")]
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

        /// <summary>
        /// Retrieves all available genders.
        /// </summary>
        /// <returns>A list of genders.</returns>
        /// <response code="200">Returns the list of genders.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GenderResponseDTO>), 200)]
        public async Task<ActionResult<IEnumerable<GenderResponseDTO>>> GetAllGenders()
        {
            var genders = await _genderService.GetAllGenders();
            return Ok(_mapper.Map<IEnumerable<GenderResponseDTO>>(genders));
        }

        /// <summary>
        /// Retrieves a gender by ID.
        /// </summary>
        /// <param name="id">The gender ID.</param>
        /// <returns>The gender details.</returns>
        /// <response code="200">Returns the gender.</response>
        /// <response code="404">If the gender is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GenderResponseDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<GenderResponseDTO>> GetGenderById(int id)
        {
            var gender = await _genderService.GetGenderById(id);
            if (gender == null)
                return NotFound(new { error = "Gender not found." });

            return Ok(_mapper.Map<GenderResponseDTO>(gender));
        }

        /// <summary>
        /// Creates a new gender.
        /// </summary>
        /// <param name="genderRequest">The gender details.</param>
        /// <returns>The created gender.</returns>
        /// <response code="201">Returns the newly created gender.</response>
        /// <response code="400">Invalid request. Gender name is required.</response>
        [HttpPost]
        [ProducesResponseType(typeof(GenderResponseDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddGender([FromBody] GenderRequestDTO genderRequest)
        {
            if (genderRequest == null || string.IsNullOrWhiteSpace(genderRequest.Name))
                return BadRequest(new { error = "Gender name is required." });

            var gender = _mapper.Map<Gender>(genderRequest);
            await _genderService.AddGender(gender);

            return CreatedAtAction(nameof(GetGenderById), new { id = gender.Id }, _mapper.Map<GenderResponseDTO>(gender));
        }

        /// <summary>
        /// Updates an existing gender.
        /// </summary>
        /// <param name="id">The gender ID.</param>
        /// <param name="genderRequest">The updated gender details.</param>
        /// <response code="200">Returns success message.</response>
        /// <response code="400">Invalid request. Gender name is required.</response>
        /// <response code="404">If the gender is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateGender(int id, [FromBody] GenderRequestDTO genderRequest)
        {
            if (genderRequest == null || string.IsNullOrWhiteSpace(genderRequest.Name))
                return BadRequest(new { error = "Gender name is required." });

            var existingGender = await _genderService.GetGenderById(id);
            if (existingGender == null)
                return NotFound(new { error = "Gender not found." });

            var gender = _mapper.Map<Gender>(genderRequest);
            gender.Id = id;
            await _genderService.UpdateGender(gender, id);

            return Ok(new { message = "Gender updated successfully." });
        }

        /// <summary>
        /// Deletes a gender by ID.
        /// </summary>
        /// <param name="id">The gender ID.</param>
        /// <response code="200">Gender deleted successfully.</response>
        /// <response code="404">If the gender is not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteGender(int id)
        {
            var existingGender = await _genderService.GetGenderById(id);
            if (existingGender == null)
                return NotFound(new { error = "Gender not found." });

            await _genderService.DeleteGender(id);
            return Ok(new { message = "Gender deleted successfully." });
        }
    }
}
