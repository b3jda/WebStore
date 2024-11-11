using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.DTOs;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandResponseDTO>>> GetAllBrands()
        {
            var brands = await _brandService.GetAllBrands();
            var brandDTOs = _mapper.Map<IEnumerable<BrandResponseDTO>>(brands);
            return Ok(brandDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandResponseDTO>> GetBrandById(int id)
        {
            var brand = await _brandService.GetBrandById(id);
            if (brand == null)
                return NotFound();

            var brandDTO = _mapper.Map<BrandResponseDTO>(brand);
            return Ok(brandDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AddBrand([FromBody] BrandRequestDTO brandRequest)
        {
            var brand = _mapper.Map<Brand>(brandRequest);
            await _brandService.AddBrand(brand);
            return CreatedAtAction(nameof(GetBrandById), new { id = brand.Id }, brand);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBrand(int id, [FromBody] BrandRequestDTO brandRequest)
        {
            var brand = _mapper.Map<Brand>(brandRequest);
            await _brandService.UpdateBrand(brand, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            await _brandService.DeleteBrand(id);
            return NoContent();
        }
    }
}
