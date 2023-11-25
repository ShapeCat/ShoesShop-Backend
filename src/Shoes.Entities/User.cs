namespace ShoesShop.Entities
{
    public enum Roles
    {
        User,
        Manager,
        Administrator,
    }
    public class User
    {
        public Guid UserId { get; set; }
        public Guid? AddressId { get; set; }
        public string UserName { get; set; }
        public Roles Role { get; set; } = Roles.User;
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public string? Phone { get; set; }

        public Address? Address { get; set; }
        public FavoritesList Favorites { get; set; } = new FavoritesList();
        public ICollection<ShopCart> ShopCarts { get; set; } = new List<ShopCart>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
