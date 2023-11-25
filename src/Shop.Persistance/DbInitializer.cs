using Microsoft.EntityFrameworkCore;

namespace ShoesShop.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(ShopDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            dbContext.FixUserNullableFields().AddUserRoles();
        }

        #region DatabaseFixes
        private static ShopDbContext FixUserNullableFields(this ShopDbContext dbContext)
        {
            dbContext.Database.ExecuteSqlRaw(@"
            ALTER TABLE [dbo].[users]
            ALTER COLUMN [Phone] NVARCHAR(50) NULL;
            ALTER TABLE [dbo].[users]
            ALTER COLUMN [AddressId] UNIQUEIDENTIFIER NULL;
            ");
            return dbContext;
        }

        private static ShopDbContext AddUserRoles(this ShopDbContext dbContext)
        {
            try
            {
                dbContext.Database.ExecuteSqlRaw(@"
            ALTER TABLE [dbo].[users]
            ADD [Role] INT NOT NULL DEFAULT 0;
            ");
            }
            catch { }
            return dbContext;
        }
        #endregion
    }
}
