using System.Security.Cryptography;
using System.Text;
using ShoesShop.Entities;
using ShoesShop.Persistence;

namespace ShoesShop.WebApi.Authentication
{
    internal static class RolesInitializer
    {
        public static ShopDbContext AddRole(this ShopDbContext dbContext, Roles role, string login, string password)
        {
            if (!dbContext.Users.Any(x => x.Login == login))
            {
                var user = new User()
                {
                    UserId = Guid.NewGuid(),
                    UserName = login + role,
                    Role = role,
                    Login = login,
                    Password = SHA256.HashData(Encoding.UTF8.GetBytes(password)),
                };
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
            return dbContext;
        }
    }
}
