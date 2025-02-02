namespace WebStore.DTOs
{
    public class OrderPlacedEventDTO
    {
        public string Message { get; set; } = "Order placed successfully!";
        public string UserId { get; set; }
        public List<OrderItemRequestDTO> OrderItems { get; set; }
    }
}
