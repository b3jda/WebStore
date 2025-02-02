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
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product?> GetProductById(int productId)
        {
            return await _context.Products
                .AsNoTracking() 
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
                .AsNoTracking() 
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
            if (product == null) throw new ArgumentNullException(nameof(product));

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product, int productId)
        {
            var existingProduct = await _context.Products.FindAsync(productId);
            if (existingProduct == null) throw new Exception("Product not found.");

           
            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int productId)
        {
            var existingProduct = await _context.Products.FindAsync(productId);
            if (existingProduct == null) throw new Exception("Product not found.");

            _context.Products.Remove(existingProduct);
            await _context.SaveChangesAsync();
        }

        public async Task ApplyDiscount(int productId, double discountPercentage)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new Exception("Product not found.");

            if (!product.IsDiscounted)
            {
                product.OriginalPrice = product.Price;
            }

            product.Price = product.OriginalPrice - (product.OriginalPrice * ((decimal)discountPercentage / 100));
            product.DiscountPercentage = discountPercentage;
            product.IsDiscounted = true;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveDiscount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new Exception("Product not found.");
            if (!product.IsDiscounted) throw new Exception("Product is not discounted.");

            product.Price = product.OriginalPrice;
            product.IsDiscounted = false;
            product.DiscountPercentage = null;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> SearchProducts(
            string? category = null,
            string? gender = null,
            string? brand = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string? size = null,
            string? color = null,
            bool? inStock = null)
        {
            var query = _context.Products
                .AsNoTracking()
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

        public async Task<ProductStockDTO?> UpdateStock(int productId, int quantitySold)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new Exception("Product not found.");

            if (product.Quantity < quantitySold)
            {
                throw new Exception($"Not enough stock available. Current stock: {product.Quantity}");
            }

            product.Quantity -= quantitySold;
            await _context.SaveChangesAsync();

            return new ProductStockDTO
            {
                ProductId = product.Id,
                Name = product.Name,
                InitialQuantity = product.Quantity + quantitySold,
                SoldQuantity = quantitySold,
                CurrentQuantity = product.Quantity
            };
        }

        public async Task<ProductStockDTO?> GetRealTimeProductStock(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return null;

            var soldQuantity = await _context.OrderItems
                .Where(oi => oi.ProductId == productId && oi.Order.Status == OrderStatus.Completed)
                .SumAsync(oi => oi.Quantity);

            return new ProductStockDTO
            {
                ProductId = product.Id,
                Name = product.Name,
                InitialQuantity = product.Quantity,
                SoldQuantity = soldQuantity,
                CurrentQuantity = product.Quantity - soldQuantity
            };
        }

        public async Task<List<Product>> GetDiscountedProductsAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.IsDiscounted)
                .ToListAsync();
        }
    }
}
