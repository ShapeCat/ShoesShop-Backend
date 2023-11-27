namespace ShoesShop.Entities
{
    public class ShopCart
    {
        public Guid ShopCartId { get; set; }
        public Guid UserId { get; set; }

        public User Owner { get; }
        public ICollection<ShopCartItem> Items { get; }

        public ShopCart(Guid shopCartId, Guid userId)
        {
            (ShopCartId, UserId)
                = (shopCartId, userId);
        }

        public ShopCart(Guid userId)
            : this(Guid.NewGuid(), userId) { }
    }
}
