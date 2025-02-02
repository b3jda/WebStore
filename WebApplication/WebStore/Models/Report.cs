using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        public DateTime ReportDate { get; set; }

        public decimal TotalEarnings { get; set; }

        [ForeignKey("Product")]
        public int MostSellingProductId { get; set; }
        public Product MostSellingProduct { get; set; }
    }
}
