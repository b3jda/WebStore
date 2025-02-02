namespace WebStore.DTOs
{
    public class OrderItemRequestDTO
    {
        public int ProductId { get; set; } // Unique identifier of the product
        public int Quantity { get; set; } // Number of units ordered
        public decimal UnitPrice { get; set; } // Price per unit at the time of order
    }
}
