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
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public Guid? AddressId { get; set; }
        public string UserName { get; set; }
        public Roles Role { get; set; }
        public string? Phone { get; set; }

        public Address? Address { get; }
        public IEnumerable<FavoritesItem> Favorites { get; }
        public ICollection<ShopCartItem> ShopCartItems { get; }
        public ICollection<Order> Orders { get; }
        public ICollection<Review> Reviews { get; set; }

        public User(Guid userId, string login, byte[] password, Guid? addressId, string userName, Roles role, string? phone)
        {
            (UserId, Role, Login, Password, UserName, AddressId, Phone)
                = (userId, role, login, password, userName, addressId, phone);
        }

        public User(string login, byte[] password, Roles role = Roles.User, Guid? addressId = null, string? phone = null, string? userName = null)
             : this(Guid.NewGuid(), login, password, addressId, userName ?? GenerateNewUsername(), role, phone) { }

        public User(string login, string password, Roles role = Roles.User, Guid? addressId = null, string? phone = null, string? userName = null)
            : this(Guid.NewGuid(), login, HashPassword(password), addressId, userName ?? GenerateNewUsername(), role, phone) { }


        public static string GenerateNewUsername() => $"User {new Random().Next(0, 99999)}";

        public static byte[] HashPassword(string password) => SHA256.HashData(Encoding.UTF8.GetBytes(password));

        public bool IsValidPassword(string password)
        {
            var hashedPassword = HashPassword(password);
            for (var i = 0; i < password.Length; i++)
            {
                if (Password[i] != hashedPassword[i]) return false;
            }
            return true;
        }
    }
}
