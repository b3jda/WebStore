namespace WebStore.DTOs
{
    public class DiscountResponseDTO
    {
        public int Id { get; set; }
        public double DiscountPercentage { get; set; }
        public int ProductId { get; set; }
        public ProductDiscountResponseDTO Product { get; set; }
    }
}
