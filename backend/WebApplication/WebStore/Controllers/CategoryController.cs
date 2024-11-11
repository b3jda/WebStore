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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponseDTO>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            var categoryDTOs = _mapper.Map<IEnumerable<CategoryResponseDTO>>(categories);
            return Ok(categoryDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponseDTO>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();

            var categoryDTO = _mapper.Map<CategoryResponseDTO>(category);
            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory([FromBody] CategoryRequestDTO categoryRequest)
        {
            var category = _mapper.Map<Category>(categoryRequest);
            await _categoryService.AddCategory(category);
            var categoryResponse = _mapper.Map<CategoryResponseDTO>(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, categoryResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryRequestDTO categoryRequest)
        {
            var category = _mapper.Map<Category>(categoryRequest);
            await _categoryService.UpdateCategory(category, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }
    }
}
