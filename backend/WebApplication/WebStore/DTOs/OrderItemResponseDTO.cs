using WebStore.Models;

namespace WebStore.DTOs
{
    public class OrderItemResponseDTO
    {
        public OrderStatus Status { get; set; } // Only status is included for updates
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
