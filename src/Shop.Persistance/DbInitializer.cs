using Microsoft.EntityFrameworkCore;

namespace ShoesShop.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(ShopDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            FixUserNullableFields(dbContext);
        }


        public static void FixUserNullableFields(ShopDbContext dbContext)
        {
            dbContext.Database.ExecuteSqlRaw(@"
            ALTER TABLE [dbo].[users]
            ALTER COLUMN [Phone] NVARCHAR(50) NULL;
            ALTER TABLE [dbo].[users]
            ALTER COLUMN [AddressId] UNIQUEIDENTIFIER NULL;
            ");
        }
    }
}
