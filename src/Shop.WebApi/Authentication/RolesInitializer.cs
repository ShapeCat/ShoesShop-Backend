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
                var user = new User(login, password, role: role, userName: login);
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
            return dbContext;
        }
        }
    }
