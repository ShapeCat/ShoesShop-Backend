using System.Security.Cryptography;
using System.Text;

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

        public static string GenerateNewUsername() => $"User {new Random().Next(0, 99999)}";

        public static byte[] HashPassword(string password) => SHA256.HashData(Encoding.UTF8.GetBytes(password));

        public bool IsValidPassword(byte[] password)
        {
            for (var i = 0; i < password.Length; i++)
            {
                if (Password[i] != password[i]) return false;
            }
            return true;
        }
    }
}
