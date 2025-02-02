using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Models;
using WebStore.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Repositories.Implementations
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly AppDbContext _context;

        public DiscountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Discount>> GetAllDiscounts()
        {
            return await _context.Discounts
                .Include(d => d.Product) 
                    .ThenInclude(p => p.Category) 
                .Include(d => d.Product.Brand) 
                .Include(d => d.Product.Gender) 
                .Include(d => d.Product.Color) 
                .Include(d => d.Product.Size) 
                .ToListAsync();
        }

        public async Task<Discount> GetDiscountById(int discountId)
        {
            return await _context.Discounts
                .Include(d => d.Product) // Include Product entity
                    .ThenInclude(p => p.Category) // Include Category if needed
                .Include(d => d.Product.Brand) // Include Brand if needed
                .Include(d => d.Product.Gender) // Include Gender if needed
                .Include(d => d.Product.Color) // Include Color if needed
                .Include(d => d.Product.Size) // Include Size if needed
                .FirstOrDefaultAsync(d => d.Id == discountId);
        }

        public async Task<Discount> AddDiscount(Discount discount)
        {
            // Ensure the product ID is valid before adding the discount
            var product = await _context.Products.FindAsync(discount.ProductId);
            if (product == null)
            {
                throw new Exception("Invalid ProductId. The product does not exist.");
            }

            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();

            // Return the added discount with the included product
            return await _context.Discounts
                .Include(d => d.Product)
                .FirstOrDefaultAsync(d => d.Id == discount.Id);
        }

        public async Task UpdateDiscount(Discount discount, int discountId)
        {
            var existingDiscount = await _context.Discounts
                .Include(d => d.Product)
                .FirstOrDefaultAsync(d => d.Id == discountId);
            if (existingDiscount != null)
            {
                existingDiscount.DiscountPercentage = discount.DiscountPercentage;
                existingDiscount.ProductId = discount.ProductId;
                _context.Discounts.Update(existingDiscount);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDiscount(int discountId)
        {
            var existingDiscount = await _context.Discounts.FindAsync(discountId);
            if (existingDiscount != null)
            {
                _context.Discounts.Remove(existingDiscount);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Discount> GetDiscountForProduct(int productId)
        {
            return await _context.Discounts
                .Include(d => d.Product) 
                    .ThenInclude(p => p.Category) 
                .Include(d => d.Product.Brand)
                .Include(d => d.Product.Gender) 
                .Include(d => d.Product.Color)
                .Include(d => d.Product.Size) 
                .FirstOrDefaultAsync(d => d.ProductId == productId);
        }
    }
}
