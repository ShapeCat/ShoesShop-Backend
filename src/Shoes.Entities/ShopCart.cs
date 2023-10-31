namespace ShoesShop.Entities
{
    public class ShopCart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public User Owner { get; set; }
        public ICollection<CartItem> Items { get; set; }
    }
}
