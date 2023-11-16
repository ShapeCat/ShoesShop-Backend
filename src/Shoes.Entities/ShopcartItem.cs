namespace ShoesShop.Entities
{
    public class ShopcartItem
    {
        public Guid Id { get; set; }
        public Guid ShopcartId { get; set; }
        public Guid ModeVariantId { get; set; }
        public int Amount { get; set; }

        public Shopcart Shopcart { get; set; }
        public ModelVariant ModelVariant { get; set; }
    }
}
