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
    /// Manages size-related operations in the WebStore API.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/size")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SizeController"/> class.
        /// </summary>
        /// <param name="sizeService">The service that handles size-related operations.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public SizeController(ISizeService sizeService, IMapper mapper)
        {
            _sizeService = sizeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all available sizes.
        /// </summary>
        /// <returns>A list of sizes.</returns>
        /// <response code="200">Returns the list of sizes successfully.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<SizeResponseDTO>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<SizeResponseDTO>>> GetAllSizes()
        {
            try
            {
                var sizes = await _sizeService.GetAllSizes();
                var sizeDTOs = _mapper.Map<IEnumerable<SizeResponseDTO>>(sizes);
                return Ok(sizeDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a specific size by its ID.
        /// </summary>
        /// <param name="id">The ID of the size to retrieve.</param>
        /// <returns>The size details if found; otherwise, a 404 response.</returns>
        /// <response code="200">Returns the size details successfully.</response>
        /// <response code="404">Size not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(SizeResponseDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<SizeResponseDTO>> GetSizeById(int id)
        {
            try
            {
                var size = await _sizeService.GetSizeById(id);
                if (size == null)
                    return NotFound(new { error = "Size not found." });

                var sizeDTO = _mapper.Map<SizeResponseDTO>(size);
                return Ok(sizeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Adds a new size.
        /// </summary>
        /// <param name="sizeRequest">The size details to add.</param>
        /// <returns>The created size with a 201 response.</returns>
        /// <response code="201">Size created successfully.</response>
        /// <response code="400">Invalid request. Ensure all required fields are provided.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(SizeResponseDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AddSize([FromBody] SizeRequestDTO sizeRequest)
        {
            try
            {
                if (sizeRequest == null)
                    return BadRequest(new { error = "Invalid request. Size details are required." });

                var size = _mapper.Map<Size>(sizeRequest);
                await _sizeService.AddSize(size);
                var sizeResponse = _mapper.Map<SizeResponseDTO>(size);

                return CreatedAtAction(nameof(GetSizeById), new { id = size.Id, version = "1.0" }, sizeResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing size.
        /// </summary>
        /// <param name="id">The ID of the size to update.</param>
        /// <param name="sizeRequest">The updated size details.</param>
        /// <returns>A 204 No Content response if successful.</returns>
        /// <response code="204">Size updated successfully.</response>
        /// <response code="400">Invalid request. Ensure all required fields are provided.</response>
        /// <response code="404">Size not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateSize(int id, [FromBody] SizeRequestDTO sizeRequest)
        {
            try
            {
                if (sizeRequest == null)
                    return BadRequest(new { error = "Invalid request. Size details are required." });

                var size = _mapper.Map<Size>(sizeRequest);
                await _sizeService.UpdateSize(size, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a specific size by its ID.
        /// </summary>
        /// <param name="id">The ID of the size to delete.</param>
        /// <returns>A confirmation message if successful.</returns>
        /// <response code="200">Size deleted successfully.</response>
        /// <response code="404">Size not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteSize(int id)
        {

            try
            {
                var size = await _sizeService.GetSizeById(id);
                if (size == null)
                    return NotFound(new { error = "Size not found." });

                await _sizeService.DeleteSize(id);
                return Ok(new { message = "Size deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }
    }
}