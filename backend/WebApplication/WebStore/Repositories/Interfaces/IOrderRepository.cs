using WebStore.Models;

namespace WebStore.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderById(int orderId);
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
        Task AddOrder(Order order);
        Task UpdateOrderStatus(int orderId, OrderStatus status);
        Task DeleteOrder(int orderId);

    }
}
