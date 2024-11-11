using System.ComponentModel.DataAnnotations;
namespace WebStore.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
