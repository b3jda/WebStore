using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    /// <summary>
    /// Manages category-related operations.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of all available categories.</returns>
        /// <response code="200">Returns the list of categories.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseDTO>), 200)]
        public async Task<ActionResult<IEnumerable<CategoryResponseDTO>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(_mapper.Map<IEnumerable<CategoryResponseDTO>>(categories));
        }

        /// <summary>
        /// Retrieves a category by ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>The category details.</returns>
        /// <response code="200">Returns the category.</response>
        /// <response code="404">If the category is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryResponseDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CategoryResponseDTO>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound(new { error = "Category not found." });

            return Ok(_mapper.Map<CategoryResponseDTO>(category));
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryRequest">The category details.</param>
        /// <returns>The created category.</returns>
        /// <response code="201">Returns the newly created category.</response>
        /// <response code="400">Invalid request. Category name is required.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CategoryResponseDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> AddCategory([FromBody] CategoryRequestDTO categoryRequest)
        {
            if (categoryRequest == null || string.IsNullOrWhiteSpace(categoryRequest.Name))
                return BadRequest(new { error = "Category name is required." });

            var category = _mapper.Map<Category>(categoryRequest);
            await _categoryService.AddCategory(category);

            var categoryResponse = _mapper.Map<CategoryResponseDTO>(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, categoryResponse);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <param name="categoryRequest">The updated category details.</param>
        /// <response code="200">Returns success message.</response>
        /// <response code="400">Invalid request. Category name is required.</response>
        /// <response code="404">If the category is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryRequestDTO categoryRequest)
        {
            if (categoryRequest == null || string.IsNullOrWhiteSpace(categoryRequest.Name))
                return BadRequest(new { error = "Category name is required." });

            var existingCategory = await _categoryService.GetCategoryById(id);
            if (existingCategory == null)
                return NotFound(new { error = "Category not found." });

            var category = _mapper.Map<Category>(categoryRequest);
            category.Id = id;
            await _categoryService.UpdateCategory(category, id);

            return Ok(new { message = "Category updated successfully." });
        }

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <response code="200">Category deleted successfully.</response>
        /// <response code="404">If the category is not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var existingCategory = await _categoryService.GetCategoryById(id);
            if (existingCategory == null)
                return NotFound(new { error = "Category not found." });

            await _categoryService.DeleteCategory(id);
            return Ok(new { message = "Category deleted successfully." });
        }
    }
}
