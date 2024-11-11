using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Range(0, 100)]
        public double DiscountPercentage { get; set; }

        [ForeignKey("Products")]
        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
