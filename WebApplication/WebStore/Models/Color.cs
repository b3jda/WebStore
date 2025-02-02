using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
