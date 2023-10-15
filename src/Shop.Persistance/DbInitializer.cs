namespace ShoesShop.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(ShopDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
