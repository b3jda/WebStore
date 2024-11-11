using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.DTOs;
using AutoMapper;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColorResponseDTO>>> GetAllColors()
        {
            var colors = await _colorService.GetAllColors();
            var colorDTOs = _mapper.Map<IEnumerable<ColorResponseDTO>>(colors);
            return Ok(colorDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ColorResponseDTO>> GetColorById(int id)
        {
            var color = await _colorService.GetColorById(id);
            if (color == null)
                return NotFound();

            var colorDTO = _mapper.Map<ColorResponseDTO>(color);
            return Ok(colorDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AddColor([FromBody] ColorRequestDTO colorRequest)
        {
            var color = _mapper.Map<Color>(colorRequest);
            await _colorService.AddColor(color);
            var colorResponse = _mapper.Map<ColorResponseDTO>(color);
            return CreatedAtAction(nameof(GetColorById), new { id = color.Id }, colorResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateColor(int id, [FromBody] ColorRequestDTO colorRequest)
        {
            var color = _mapper.Map<Color>(colorRequest);
            await _colorService.UpdateColor(color, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteColor(int id)
        {
            await _colorService.DeleteColor(id);
            return NoContent();
        }
    }
}
