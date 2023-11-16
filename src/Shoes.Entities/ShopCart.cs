namespace ShoesShop.Entities
{
    public class Shopcart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public User Owner { get; set; }
        public ICollection<ShopcartItem> Items { get; set; }
    }
}
