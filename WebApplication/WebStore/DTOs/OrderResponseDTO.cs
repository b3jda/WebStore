using System.Text.Json.Serialization;
using WebStore.Models;

namespace WebStore.DTOs
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public UserResponseDTO User { get; set; }
        public List<OrderItemResponseDTO> OrderItems { get; set; }
        public decimal TotalPrice { get; set; } // Total calculated in the response
      
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status { get; set; } // Displaying order status
        public DateTime? EstimatedDeliveryDate { get; set; } //GraphQL additional(Query)

        /// <summary>
        /// HATEOAS Links for navigation
        /// </summary>
        public List<LinkDTO> Links { get; set; } = new();
    }
}
