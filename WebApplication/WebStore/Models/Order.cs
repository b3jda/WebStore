using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; } // Pending, Shipped, Completed, Cancelled..

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        // Navigation property: One-to-Many relationship with OrderItems
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        //Dynamically calculates the sum of all order items based on their quantity and price
        [NotMapped]
        public decimal TotalPrice => OrderItems.Sum(item => item.Quantity * item.UnitPrice);
    }
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled,
        Completed
    }
}
