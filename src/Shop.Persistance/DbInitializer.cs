using Microsoft.EntityFrameworkCore;

namespace ShoesShop.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(ShopDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            dbContext.FixUserNullableFields().AddUserRoles().FixFavoriteItemKeys().DropShopCarts();
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

        private static ShopDbContext DropShopCarts(this ShopDbContext dbContext)
        {
            try
            {
                dbContext.Database.ExecuteSqlRaw(@"
                    CREATE TABLE [dbo].[shop_carts_items_new] (
                    [ShopCartItemId] UNIQUEIDENTIFIER PRIMARY KEY,
                    [UserId]         UNIQUEIDENTIFIER NOT NULL,
                    [ModeVariantId]  UNIQUEIDENTIFIER NOT NULL,
                    [Amount]         INT              NOT NULL,
                    CONSTRAINT [FK_shop_cart_items_new_users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[users] ([UserId]),
                    CONSTRAINT [FK_shop_cart_items_new_models_variants_ModeVariantId] FOREIGN KEY ([ModeVariantId]) REFERENCES [dbo].[models_variants] ([ModelVariantId])
                    );

                INSERT INTO [dbo].[shop_carts_items_new] (
                    [ShopCartItemId],
                    [UserId],
                    [ModeVariantId],
                    [Amount]
                    ) 
                SELECT
                    [sci].[ShopCartItemId],
                    [sc].[UserId],
                    [sci].[ModeVariantId],
                    [sci].[Amount]
                FROM
                    [dbo].[shop_carts_items] AS [sci]
                    INNER JOIN [dbo].[shop_carts] AS [sc] ON [sci].[ShopCartId] = [sc].[ShopCartId];
                
                ALTER TABLE [dbo].[shop_carts_items]
                    DROP CONSTRAINT [FK_shop_carts_items_shop_carts_ShopCartId];

                DROP TABLE [dbo].[shop_carts];
                DROP TABLE [dbo].[shop_carts_items];
                EXEC sp_rename 'dbo.shop_carts_items_new', 'shop_carts_items';
                ");
            }
            catch{ }
            return dbContext;
        }
    }
}
