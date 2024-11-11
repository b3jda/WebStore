using WebStore.DTOs;
using WebStore.Models;

namespace WebStore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDTO> GetOrderById(int orderId);
        Task<IEnumerable<OrderResponseDTO>> GetAllOrders();
        Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserId(string userId);
        Task<OrderResponseDTO> AddOrder(OrderRequestDTO orderRequest);
        Task UpdateOrderStatus(int orderId, OrderStatus status);
        Task DeleteOrder(int orderId);
    }
}
