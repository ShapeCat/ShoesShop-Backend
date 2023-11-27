using Microsoft.EntityFrameworkCore;

namespace ShoesShop.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(ShopDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            dbContext.FixUserNullableFields().AddUserRoles().FixFavoriteItemKeys();
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

        private static ShopDbContext FixFavoriteItemKeys(this ShopDbContext dbContext)
        {
            try
            {
                dbContext.Database.ExecuteSqlRaw(@"
                ALTER TABLE [dbo].[favorites_items]
                    ADD CONSTRAINT [FK_favorites_items_model_variants]
                    FOREIGN KEY ([ModelVariantId])
                    REFERENCES [dbo].[models_variants] ([ModelVariantId]);
            ");
            }
            catch { }
            return dbContext;
        }
    }
}
