namespace ShoesShop.Entities
{
    public class ShopCartItem
    {
        public Guid ShopCartItemId { get; set; }
        public Guid ShopCartId { get; set; }
        public Guid ModeVariantId { get; set; }
        public int Amount { get; set; }

        public ShopCart ShopCart { get; set; }
        public ModelVariant ModelVariant { get; set; }
    }
}
