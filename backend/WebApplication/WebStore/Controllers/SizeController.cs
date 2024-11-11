using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;
        private readonly IMapper _mapper;

        public SizeController(ISizeService sizeService, IMapper mapper)
        {
            _sizeService = sizeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SizeResponseDTO>>> GetAllSizes()
        {
            var sizes = await _sizeService.GetAllSizes();
            var sizeDTOs = _mapper.Map<IEnumerable<SizeResponseDTO>>(sizes);
            return Ok(sizeDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SizeResponseDTO>> GetSizeById(int id)
        {
            var size = await _sizeService.GetSizeById(id);
            if (size == null)
                return NotFound();

            var sizeDTO = _mapper.Map<SizeResponseDTO>(size);
            return Ok(sizeDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AddSize([FromBody] SizeRequestDTO sizeRequest)
        {
            var size = _mapper.Map<Size>(sizeRequest);
            await _sizeService.AddSize(size);
            var sizeResponse = _mapper.Map<SizeResponseDTO>(size);
            return CreatedAtAction(nameof(GetSizeById), new { id = size.Id }, sizeResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSize(int id, [FromBody] SizeRequestDTO sizeRequest)
        {
            var size = _mapper.Map<Size>(sizeRequest);
            await _sizeService.UpdateSize(size, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSize(int id)
        {
            await _sizeService.DeleteSize(id);
            return NoContent();
        }
    }
}
