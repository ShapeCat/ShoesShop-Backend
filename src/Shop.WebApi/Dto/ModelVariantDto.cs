namespace ShoesShop.WebApi.Dto
{
    public class ModelVariantDto
    {
        public Guid ModelId { get; set; }
        public Guid ModelSizeId { get; set; }
        public int ItemsLeft { get; set; }
        public decimal Price { get; set; }
    }
}
