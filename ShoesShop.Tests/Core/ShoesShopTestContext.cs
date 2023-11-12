using Microsoft.EntityFrameworkCore;
using ShoesShop.Persistence;

namespace ShoesShop.Tests.Core
{
    internal static class ShoesShopTestContext
    {
        public static ShopDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ShopDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new ShopDbContext(options);
            dbContext.Database.EnsureCreated();
            TestData.SeedData(dbContext);
            return dbContext;
        }

        public static void Destroy(ShopDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
