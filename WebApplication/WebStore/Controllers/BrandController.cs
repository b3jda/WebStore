using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.DTOs;
using WebStore.Models;
using WebStore.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WebStore.Controllers
{
    /// <summary>
    /// Manages brand-related operations.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/brand")]
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

        /// <summary>
        /// Retrieves all brands.
        /// </summary>
        /// <returns>A list of all available brands.</returns>
        /// <response code="200">Returns the list of brands.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BrandResponseDTO>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<BrandResponseDTO>>> GetAllBrands()
        {
            try
            {
                var brands = await _brandService.GetAllBrands();
                return Ok(_mapper.Map<IEnumerable<BrandResponseDTO>>(brands));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a brand by ID.
        /// </summary>
        /// <param name="id">The brand ID.</param>
        /// <returns>The brand details.</returns>
        /// <response code="200">Returns the brand.</response>
        /// <response code="404">If the brand is not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BrandResponseDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<BrandResponseDTO>> GetBrandById(int id)
        {
            try
            {
                var brand = await _brandService.GetBrandById(id);
                if (brand == null)
                    return NotFound(new { error = "Brand not found." });

                return Ok(_mapper.Map<BrandResponseDTO>(brand));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new brand.
        /// </summary>
        /// <param name="brandRequest">The brand details.</param>
        /// <returns>The created brand.</returns>
        /// <response code="201">Returns the newly created brand.</response>
        /// <response code="400">Invalid request. Brand name is required.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(BrandResponseDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AddBrand([FromBody] BrandRequestDTO brandRequest)
        {
            try
            {
                if (brandRequest == null || string.IsNullOrWhiteSpace(brandRequest.Name))
                    return BadRequest(new { error = "Brand name is required." });

                var brand = _mapper.Map<Brand>(brandRequest);
                await _brandService.AddBrand(brand);

                return CreatedAtAction(
                    nameof(GetBrandById),
                    new { id = brand.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() },
                    _mapper.Map<BrandResponseDTO>(brand)
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing brand.
        /// </summary>
        /// <param name="id">The brand ID.</param>
        /// <param name="brandRequest">The updated brand details.</param>
        /// <response code="200">Brand updated successfully.</response>
        /// <response code="400">Invalid request. Brand name is required.</response>
        /// <response code="404">If the brand is not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateBrand(int id, [FromBody] BrandRequestDTO brandRequest)
        {
            try
            {
                if (brandRequest == null || string.IsNullOrWhiteSpace(brandRequest.Name))
                    return BadRequest(new { error = "Brand name is required." });

                var existingBrand = await _brandService.GetBrandById(id);
                if (existingBrand == null)
                    return NotFound(new { error = "Brand not found." });

                var brand = _mapper.Map<Brand>(brandRequest);
                brand.Id = id;
                await _brandService.UpdateBrand(brand, id);

                return Ok(new { message = "Brand updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a brand by ID.
        /// </summary>
        /// <param name="id">The brand ID.</param>
        /// <response code="200">Brand deleted successfully.</response>
        /// <response code="404">If the brand is not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            try
            {
                var existingBrand = await _brandService.GetBrandById(id);
                if (existingBrand == null)
                    return NotFound(new { error = "Brand not found." });

                await _brandService.DeleteBrand(id);
                return Ok(new { message = "Brand deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }
    }
}