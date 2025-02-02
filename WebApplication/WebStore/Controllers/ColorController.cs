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
    /// Manages color-related operations.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/color")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;
        private readonly IMapper _mapper;

        public ColorController(IColorService colorService, IMapper mapper)
        {
            _colorService = colorService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all available colors.
        /// </summary>
        /// <returns>A list of colors.</returns>
        /// <response code="200">Returns the list of colors.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ColorResponseDTO>), 200)]
        public async Task<ActionResult<IEnumerable<ColorResponseDTO>>> GetAllColors()
        {
            var colors = await _colorService.GetAllColors();
            return Ok(_mapper.Map<IEnumerable<ColorResponseDTO>>(colors));
        }

        /// <summary>
        /// Retrieves a color by ID.
        /// </summary>
        /// <param name="id">The color ID.</param>
        /// <returns>The color details.</returns>
        /// <response code="200">Returns the color.</response>
        /// <response code="404">If the color is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ColorResponseDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ColorResponseDTO>> GetColorById(int id)
        {
            var color = await _colorService.GetColorById(id);
            if (color == null)
                return NotFound(new { error = "Color not found." });

            return Ok(_mapper.Map<ColorResponseDTO>(color));
        }

        /// <summary>
        /// Creates a new color.
        /// </summary>
        /// <param name="colorRequest">The color details.</param>
        /// <returns>The created color.</returns>
        /// <response code="201">Returns the newly created color.</response>
        /// <response code="400">Invalid request. Color name is required.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ColorResponseDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddColor([FromBody] ColorRequestDTO colorRequest)
        {
            if (colorRequest == null || string.IsNullOrWhiteSpace(colorRequest.Name))
                return BadRequest(new { error = "Color name is required." });

            var color = _mapper.Map<Color>(colorRequest);
            await _colorService.AddColor(color);

            return CreatedAtAction(nameof(GetColorById), new { id = color.Id }, _mapper.Map<ColorResponseDTO>(color));
        }

        /// <summary>
        /// Updates an existing color.
        /// </summary>
        /// <param name="id">The color ID.</param>
        /// <param name="colorRequest">The updated color details.</param>
        /// <response code="200">Returns success message.</response>
        /// <response code="400">Invalid request. Color name is required.</response>
        /// <response code="404">If the color is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateColor(int id, [FromBody] ColorRequestDTO colorRequest)
        {
            if (colorRequest == null || string.IsNullOrWhiteSpace(colorRequest.Name))
                return BadRequest(new { error = "Color name is required." });

            var existingColor = await _colorService.GetColorById(id);
            if (existingColor == null)
                return NotFound(new { error = "Color not found." });

            var color = _mapper.Map<Color>(colorRequest);
            color.Id = id;
            await _colorService.UpdateColor(color, id);

            return Ok(new { message = "Color updated successfully." });
        }

        /// <summary>
        /// Deletes a color by ID.
        /// </summary>
        /// <param name="id">The color ID.</param>
        /// <response code="200">Color deleted successfully.</response>
        /// <response code="404">If the color is not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteColor(int id)
        {
            var existingColor = await _colorService.GetColorById(id);
            if (existingColor == null)
                return NotFound(new { error = "Color not found." });

            await _colorService.DeleteColor(id);
            return Ok(new { message = "Color deleted successfully." });
        }
    }
}
