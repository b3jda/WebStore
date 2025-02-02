namespace WebStore.DTOs
{
    public class ProductResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string GenderName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public bool IsDiscounted { get; set; }
        public double? DiscountPercentage { get; set; }

        /// <summary>
        /// HATEOAS Links for navigation
        /// </summary>
        public List<LinkDTO> Links { get; set; } = new();

    }
}
