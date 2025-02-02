namespace WebStore.DTOs
{
    public class OrderRequestDTO
    {
        public DateTime OrderDate { get; set; } 
        public string UserId { get; set; }
        public List<OrderItemRequestDTO> OrderItems { get; set; }
    }
}
