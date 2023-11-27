namespace ShoesShop.Entities
{
    public class Sale
    {
        public Guid SaleId { get; set; }
        public Guid ModelVariantId { get; set; }
        public float Percent { get; set; }
        public DateTime SaleEndDate { get; set; }

        public ModelVariant ModelVariant { get; }


        public Sale(Guid saleId, Guid modelVariantId, float percent, DateTime saleEndDate)
        {
            (SaleId, ModelVariantId, Percent, SaleEndDate)
                = (saleId, modelVariantId, percent, saleEndDate);
        }

        public Sale(Guid modelVariantId, float percent, DateTime saleEndDate)
            : this(Guid.NewGuid(), modelVariantId, percent, saleEndDate) { }
    }
}
