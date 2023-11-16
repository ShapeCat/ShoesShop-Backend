namespace ShoesShop.Entities
{
    public class Price
    {
        public Guid PriceId { get; set; }
        public Guid ModelVariantId { get; set; }
        public decimal BasePrice { get; set; }
        public decimal? Sale { get; set; }
        public DateTime? SaleEndDate { get; set; }

        public ModelVariant ModelVariant { get; set; }
    }
}
