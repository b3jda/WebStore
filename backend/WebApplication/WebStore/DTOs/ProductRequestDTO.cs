namespace WebStore.DTOs
{
    public class ProductRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Using names instead of IDs for easier input
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string GenderName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public double? DiscountPercentage { get; set; }
    }
}
