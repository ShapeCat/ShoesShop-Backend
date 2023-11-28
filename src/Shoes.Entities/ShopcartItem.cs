namespace ShoesShop.Entities
{
    public class ShopCartItem
    {
        public Guid ShopCartItemId { get; set; }
        public Guid UserId { get; set; }
        public Guid ModeVariantId { get; set; }
        public int Amount { get; set; }

        public User User { get; }
        public ModelVariant ModelVariant { get; }

        public ShopCartItem(Guid shopCartItemId, Guid userId, Guid modeVariantId, int amount)
        {
            (ShopCartItemId, UserId, ModeVariantId, Amount)
            = (shopCartItemId, userId, modeVariantId, amount);
        }

        public ShopCartItem(Guid userId, Guid modelVariantId, int amount)
            : this(Guid.NewGuid(), userId, modelVariantId, amount) { }
    }
}
