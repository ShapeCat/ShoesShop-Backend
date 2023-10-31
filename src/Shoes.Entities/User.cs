namespace ShoesShop.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid AdressId { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public string Phone { get; set; }

        public Adress Adress { get; set; }
        public FavoritesList Favorites { get; set; }
        public ICollection<ShopCart> ShopCarts { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Rewiews { get; set; }
    }
}
