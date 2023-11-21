namespace ShoesShop.Entities
{
    public class ShopCart
    {
        public Guid ShopCartId { get; set; }
        public Guid UserId { get; set; }

        public User Owner { get; set; }
        public ICollection<ShopCartItem> Items { get; set; }
    }
}
