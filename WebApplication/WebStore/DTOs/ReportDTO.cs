

using System.Text.Json.Serialization;

namespace WebStore.DTOs
{
    public class ReportDTO
    {
        public DateOnly ReportDate { get; set; }
        public decimal TotalEarnings { get; set; }
        public string MostSellingProductName { get; set; }
        public int MostSellingProductQuantity { get; set; }
    }
}
