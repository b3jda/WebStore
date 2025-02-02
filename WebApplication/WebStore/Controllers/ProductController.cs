using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.DTOs;
using WebStore.Services.Interfaces;
using WebStore.Services.Utilities;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace WebStore.Controllers
{
    /// <summary>
    /// Handles operations related to products.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly LinkHelper _linkHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">Service for handling products.</param>
        /// <param name="urlHelperFactory">Factory for creating URL helpers.</param>
        /// <param name="accessor">Accessor for action context.</param>
        public ProductController(IProductService productService, IUrlHelperFactory urlHelperFactory, IActionContextAccessor accessor)
        {
            _productService = productService;
            _linkHelper = new LinkHelper(urlHelperFactory.GetUrlHelper(accessor.ActionContext));
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all available products with HATEOAS links.</returns>
        /// <response code="200">Returns the list of products.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet(Name = "GetAllProducts")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProducts();

                var productList = products.Select(p =>
                {
                    p.Links = _linkHelper.GenerateProductLinks(p.Id);
                    return p;
                }).ToList();

                var collectionLinks = _linkHelper.GenerateProductCollectionLinks();

                return Ok(new
                {
                    products = productList,
                    links = collectionLinks
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>The requested product details with HATEOAS links.</returns>
        /// <response code="200">Returns the requested product.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("{id}", Name = "GetProductById")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ProductResponseDTO>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null)
                    return NotFound(new { error = "Product not found" });

                product.Links = _linkHelper.GenerateProductLinks(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Adds a new product. (Requires Admin, AdvancedUser role)
        /// </summary>
        /// <param name="productRequest">The product details.</param>
        /// <returns>The created product with HATEOAS links.</returns>
        /// <response code="201">Returns the created product.</response>
        /// <response code="400">If the product request is invalid.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <response code="403">If the user does not have the required role.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPost(Name = "AddProduct")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> AddProduct([FromBody] ProductRequestDTO productRequest)
        {
            try
            {
                var createdProduct = await _productService.AddProduct(productRequest);

                createdProduct.Links = _linkHelper.GenerateProductLinks(createdProduct.Id);

                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id, version = "1.0" }, createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing product by its ID.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <param name="productRequest">Updated product details.</param>
        /// <response code="204">If the product is updated successfully.</response>
        /// <response code="400">If the product request is invalid.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPut("{id}", Name = "UpdateProduct")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductRequestDTO productRequest)
        {
            try
            {
                await _productService.UpdateProduct(productRequest, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a product. (Requires Admin or AdvancedUser role)
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>A success message.</returns>
        /// <response code="200">If the product is deleted successfully.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <response code="403">If the user does not have the required role.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpDelete("{id}", Name = "DeleteProduct")]
        [Authorize(Roles = "Admin, AdvancedUser")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return Ok(new { message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves real-time stock information for a product.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>Stock information for the product.</returns>
        /// <response code="200">Returns the stock information.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("{id}/quantity")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ProductStockDTO>> GetRealTimeProductStock(int id)
        {
            try
            {
                var stockInfo = await _productService.GetRealTimeProductStock(id);
                if (stockInfo == null)
                    return NotFound(new { error = "Product not found" });
                return Ok(stockInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Searches for products based on filters.
        /// </summary>
        /// <param name="category">The product category.</param>
        /// <param name="gender">The product gender.</param>
        /// <param name="brand">The product brand.</param>
        /// <param name="minPrice">The minimum price.</param>
        /// <param name="maxPrice">The maximum price.</param>
        /// <param name="size">The product size.</param>
        /// <param name="color">The product color.</param>
        /// <param name="inStock">Whether the product is in stock.</param>
        /// <returns>A list of products matching the search criteria.</returns>
        /// <response code="200">Returns the list of products.</response>
        /// <response code="404">If no products are found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("search")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
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
            try
            {
                var products = await _productService.SearchProducts(category, gender, brand, minPrice, maxPrice, size, color, inStock);

                if (products == null || !products.Any())
                    return NotFound(new { error = "No products found with the given search criteria." });

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }

        /// <summary>
        /// Applies a discount to a product. (Requires Admin role)
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <param name="discountPercentage">The discount percentage.</param>
        /// <returns>The updated product details.</returns>
        /// <response code="200">Returns the updated product.</response>
        /// <response code="400">If the discount percentage is invalid.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <response code="403">If the user does not have the required role.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPost("apply-discount/{productId}", Name = "ApplyDiscount")]
        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> ApplyDiscount(int productId, [FromBody] double discountPercentage)
        {
            try
            {
                await _productService.ApplyDiscount(productId, discountPercentage);

                var updatedProduct = await _productService.GetProductById(productId);
                updatedProduct.Links = _linkHelper.GenerateProductLinks(productId);

                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Removes a discount from a product. (Requires Admin role)
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <returns>The updated product details.</returns>
        /// <response code="200">Returns the updated product.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <response code="403">If the user does not have the required role.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPost("remove-discount/{productId}", Name = "RemoveDiscount")]
        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> RemoveDiscount(int productId)
        {
            try
            {
                await _productService.RemoveDiscount(productId);

                var updatedProduct = await _productService.GetProductById(productId);
                updatedProduct.Links = _linkHelper.GenerateProductLinks(productId);

                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a list of discounted products.
        /// </summary>
        /// <returns>A list of discounted products.</returns>
        /// <response code="200">Returns the list of discounted products.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("discounted", Name = "GetDiscountedProducts")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetDiscountedProducts()
        {
            try
            {
                var discountedProducts = await _productService.GetDiscountedProductsAsync();
                return Ok(discountedProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", message = ex.Message });
            }
        }
    }
}