using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Models;
using WebStore.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders
                                 .Include(o => o.User)
                                 .Include(o => o.OrderItems)
                                 .ThenInclude(oi => oi.Product)
                                 .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders
                                 .Include(o => o.User)
                                 .Include(o => o.OrderItems)
                                 .ThenInclude(oi => oi.Product)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
            return await _context.Orders
                                 .Where(o => o.UserId == userId)
                                 .Include(o => o.OrderItems)
                                 .ThenInclude(oi => oi.Product)
                                 .ToListAsync();
        }

        public async Task AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var existingOrder = await _context.Orders.FindAsync(orderId);
            if (existingOrder != null)
            {
                existingOrder.Status = status;
                _context.Orders.Update(existingOrder);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrder(int orderId)
        {
            var existingOrder = await _context.Orders.FindAsync(orderId);
            if (existingOrder != null)
            {
                _context.Orders.Remove(existingOrder);
                await _context.SaveChangesAsync();
            }
        }
    }
}
