namespace ShoesShop.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ModelVariantId { get; set; }
        public int Amount { get; set; }

        public Order Order { get; set; }
        public ModelVariant ModelVariant { get; set; }
    }
}