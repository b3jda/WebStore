using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.DTOs;
using WebStore.Services.Implementations;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        
        public async Task<ActionResult<ProductResponseDTO>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, AdvancedUser, SimpleUser")]
        public async Task<ActionResult> AddProduct([FromBody] ProductRequestDTO productRequest)
        {
            var createdProduct = await _productService.AddProduct(productRequest);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductRequestDTO productRequest)
        {
            await _productService.UpdateProduct(productRequest, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }

        [HttpGet("{id}/quantity")]
        
        public async Task<ActionResult<ProductStockDTO>> GetRealTimeProductStock(int id)
        {
            var stockInfo = await _productService.GetRealTimeProductStock(id);
            if (stockInfo == null)

                return NotFound();

            return Ok(stockInfo);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> SearchProducts(
                   [FromQuery] string? category,
                   [FromQuery] string? gender,
                   [FromQuery] string brand,
                   [FromQuery] decimal? minPrice,
                   [FromQuery] decimal? maxPrice,
                   [FromQuery] string? size,
                   [FromQuery] string? color,
                   [FromQuery] bool? inStock)
        {
            var products = await _productService.SearchProducts(category, gender, brand, minPrice, maxPrice, size, color, inStock);

            if (products == null || !products.Any())
            {
                return NotFound("No products found with the given search criteria.");
            }

            return Ok(products);
        }

        [HttpPost("apply-discount/{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApplyDiscount(int productId, [FromBody] double discountPercentage)
        {
            try
            {
                await _productService.ApplyDiscount(productId, discountPercentage);
                return Ok("Discount applied successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
