namespace ShoesShop.Entities
{
    public class Sale
    {
        public Guid SaleId { get; set; }
        public Guid ModelVariantId { get; set; }
        public float Percent { get; set; }
        public DateTime SaleEndDate { get; set; }

        public ModelVariant ModelVariant { get; set; }
    }
}
