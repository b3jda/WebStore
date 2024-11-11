using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        // Navigation Property: One-to-Many relationship with Products
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
