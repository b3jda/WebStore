using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.DTOs;
using WebStore.Models;
using WebStore.Repositories.Interfaces;

namespace WebStore.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Gender)
                .Include(p => p.Color)
                .Include(p => p.Size)
                .Include(p => p.Discount)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Gender)
                .Include(p => p.Color)
                .Include(p => p.Size)
                .Include(p => p.Discount)
                .ToListAsync();
        }

        public async Task AddProduct(Product product)
        {
            var productEntity = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                GenderId = product.GenderId,
                ColorId = product.ColorId,
                SizeId = product.SizeId
            };


            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product, int productId)
        {
            var existingProduct = await _context.Products.FindAsync(productId);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.BrandId = product.BrandId;
                existingProduct.GenderId = product.GenderId;
                existingProduct.ColorId = product.ColorId;
                existingProduct.SizeId = product.SizeId;

                _context.Products.Update(existingProduct);

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProduct(int productId)
        {
            var existingProduct = await _context.Products.FindAsync(productId);
            if (existingProduct != null)
            {
                _context.Products.Remove(existingProduct);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ApplyDiscount(int productId, double discountPercentage)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            
            product.Price -= product.Price * ((decimal)discountPercentage / 100);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Product>> SearchProducts(
            string category = null,
            string gender = null,
            string brand = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string size = null,
            string color = null,
            bool? inStock = null)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Gender)
                .Include(p => p.Color)
                .Include(p => p.Size)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category.Name == category);

            if (!string.IsNullOrEmpty(gender))
                query = query.Where(p => p.Gender.Name == gender);

            if (!string.IsNullOrEmpty(brand))
                query = query.Where(p => p.Brand.Name == brand);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (!string.IsNullOrEmpty(size))
                query = query.Where(p => p.Size.Name == size);

            if (!string.IsNullOrEmpty(color))
                query = query.Where(p => p.Color.Name == color);

            if (inStock.HasValue)
                query = query.Where(p => (p.Quantity > 0) == inStock.Value);

            return await query.ToListAsync();
        }


        public async Task UpdateStock(int productId, int quantitySold)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.Quantity -= quantitySold;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<ProductStockDTO> GetRealTimeProductStock(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return null;
            }

            var soldQuantity = await _context.OrderItems
                .Where(oi => oi.ProductId == productId && oi.Order.Status == OrderStatus.Completed)
                .SumAsync(oi => oi.Quantity);

            var currentQuantity = product.Quantity - soldQuantity;

            return new ProductStockDTO
            {
                ProductId = product.Id,
                Name = product.Name,
                InitialQuantity = product.Quantity,
                SoldQuantity = soldQuantity,
                CurrentQuantity = currentQuantity
            };
        }
    }
}
