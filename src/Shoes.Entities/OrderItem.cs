namespace ShoesShop.Entities
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ModelVariantId { get; set; }
        public int Amount { get; set; }

        public Order Order { get; set; }
        public ModelVariant ModelVariant { get; set; }

        public OrderItem(Guid orderItemId, Guid orderId, Guid modelVariantId, int amount)
        {
            (OrderItemId, OrderId, ModelVariantId, Amount)
                = (orderItemId, orderId, modelVariantId, amount);
        }

        public OrderItem(Guid orderId, Guid modelVariantId, int amount)
            : this(Guid.NewGuid(), orderId, modelVariantId, amount) { }
    }
}