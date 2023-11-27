namespace ShoesShop.Entities
{
    public class ShopCartItem
    {
        public Guid ShopCartItemId { get; set; }
        public Guid ShopCartId { get; set; }
        public Guid ModeVariantId { get; set; }
        public int Amount { get; set; }

        public ShopCart ShopCart { get; }
        public ModelVariant ModelVariant { get; }

        public ShopCartItem(Guid shopCartItemId, Guid shopCartId, Guid modeVariantId, int amount)
        {
            (ShopCartItemId, ShopCartId, ModeVariantId, Amount)
            = (shopCartItemId, shopCartId, modeVariantId, amount);
        }

        public ShopCartItem(Guid shopCartId, Guid modelVariantId, int amount)
            : this(Guid.NewGuid(), shopCartId, modelVariantId, amount) { }
    }
}
