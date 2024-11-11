using Microsoft.EntityFrameworkCore;
using WebStore.Models;

namespace WebStore.DTOs
{
    public class ProductStockDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int InitialQuantity { get; set; }
        public int SoldQuantity { get; set; }
        public int CurrentQuantity { get; set; }
    }
}
