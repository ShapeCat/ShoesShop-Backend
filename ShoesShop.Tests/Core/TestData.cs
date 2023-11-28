using ShoesShop.Entities;
using ShoesShop.Persistence;

namespace ShoesShop.Tests.Core
{
    internal static class TestData
    {
        public static Guid DeleteAddressId { get; } = Guid.NewGuid();
        public static Guid UpdateAddressId { get; } = Guid.NewGuid();
        public static Guid DeleteCartItemId { get; } = Guid.NewGuid();
        public static Guid UpdateCartItemId { get; } = Guid.NewGuid();
        public static Guid DeleteShopCartId { get; } = Guid.NewGuid();
        public static Guid UpdateShopCartId { get; } = Guid.NewGuid();
        public static Guid DeleteFavoriteItemId { get; } = Guid.NewGuid();
        public static Guid UpdateFavoriteItemId { get; } = Guid.NewGuid();
        public static Guid DeleteFavoriteListId { get; } = Guid.NewGuid();
        public static Guid UpdateFavoriteListId { get; } = Guid.NewGuid();
        public static Guid DeleteImageId { get; } = Guid.NewGuid();
        public static Guid UpdateImageId { get; } = Guid.NewGuid();
        public static Guid DeleteModelId { get; } = Guid.NewGuid();
        public static Guid UpdateModelId { get; } = Guid.NewGuid();
        public static Guid DeleteModelSizeId { get; } = Guid.NewGuid();
        public static Guid UpdateModelSizeId { get; } = Guid.NewGuid();
        public static Guid DeleteModelVariantId { get; } = Guid.NewGuid();
        public static Guid UpdateModelVariantId { get; } = Guid.NewGuid();
        public static Guid DeleteOrderId { get; } = Guid.NewGuid();
        public static Guid UpdateOrderId { get; } = Guid.NewGuid();
        public static Guid DeleteOrderItemId { get; } = Guid.NewGuid();
        public static Guid UpdateOrderItemId { get; } = Guid.NewGuid();
        public static Guid DeletePriceId { get; } = Guid.NewGuid();
        public static Guid UpdatePriceId { get; } = Guid.NewGuid();
        public static Guid DeleteSaleId { get; } = Guid.NewGuid();
        public static Guid UpdateSaleId { get; } = Guid.NewGuid();
        public static Guid DeleteReviewId { get; } = Guid.NewGuid();
        public static Guid UpdateReviewId { get; } = Guid.NewGuid();
        public static Guid DeleteUserId { get; } = Guid.NewGuid();
        public static Guid UpdateUserId { get; } = Guid.NewGuid();
        public static int ExistedModelSize { get; } = 44;
        public static (string login, string password) ExistedLoginData { get; } = ("login1234565432", "pass1234567");
        public static Guid UserIdWithoutAddress { get; } = Guid.NewGuid();


        public static void SeedData(ShopDbContext dbContext)
        {
            dbContext.ModelsSizes.AddRange(
                new ModelSize(DeleteModelSizeId, 1),
                new ModelSize(UpdateModelSizeId, ExistedModelSize)
            );
            dbContext.Models.AddRange(
                new Model(DeleteModelId, "test name 1", "test color 1", "test brand 1", "test SkuId 1", DateTime.Now),
                new Model(UpdateModelId, "test name 2", "test color 2", "test brand 2", "test SkuId 2", DateTime.Now)
            );
            dbContext.Images.AddRange(
                new Image(DeleteImageId, DeleteModelId, false, "test url 1"),
                new Image(UpdateImageId, UpdateModelId, true, "test url 2")
            );
            dbContext.ModelsVariants.AddRange(
                new ModelVariant(DeleteModelVariantId, DeleteModelId, DeleteModelSizeId, 1, 1222),
                new ModelVariant(UpdateModelVariantId, UpdateModelId, UpdateModelSizeId, 2, 4444)
            );
            dbContext.Sales.AddRange(
                new Sale(DeleteSaleId, DeleteModelVariantId, 0.5f, DateTime.Now),
                new Sale(UpdateSaleId, UpdateModelVariantId, 0.01f, DateTime.Now)
            );
            dbContext.Addresses.AddRange(
                new Address(DeleteAddressId, "test country 1", "test city 1", "test street 1", "444", room: 1),
                new Address(UpdateAddressId,"test country 2", "test city 2", "test street 2", "444", null)
            );
            dbContext.Users.AddRange(
                    new User(DeleteUserId, ExistedLoginData.login, User.HashPassword(ExistedLoginData.password), DeleteAddressId, "test user 1", Roles.User, "test phone 1"),
                    new User(UpdateUserId, "some login", User.HashPassword("user test password"), DeleteAddressId, "test user 2", Roles.User, "test phone 2"),
                    new User(UserIdWithoutAddress, "some login", User.HashPassword("user test password"), null, "test user 2", Roles.User, "test phone 2")
            );
            dbContext.FavoritesLists.AddRange(
                new FavoritesList(DeleteFavoriteListId, DeleteUserId),
                new FavoritesList(UpdateFavoriteListId, UpdateUserId)
            );
            dbContext.FavoritesItems.AddRange(
                new FavoritesItem(DeleteFavoriteItemId, DeleteFavoriteListId),
                new FavoritesItem(UpdateFavoriteItemId, UpdateFavoriteListId)
            );
            //dbContext.ShopCarts.AddRange(
            //    new ShopCart(DeleteShopCartId, DeleteUserId),
            //    new ShopCart(UpdateShopCartId, UpdateUserId)
            //);
            dbContext.ShopCartsItems.AddRange(
                new ShopCartItem(DeleteCartItemId, DeleteUserId, DeleteModelVariantId, 1),
                new ShopCartItem(UpdateCartItemId, UpdateUserId, UpdateModelVariantId, 1)
            );
            dbContext.Orders.AddRange(
                new Order(DeleteOrderId, DeleteUserId, OrderStatus.Created, DateTime.Now),
                new Order(UpdateOrderId, UpdateUserId, OrderStatus.Finished, DateTime.Now)
            );
            dbContext.OrdersItems.AddRange(
                new OrderItem(DeleteOrderItemId, DeleteOrderId, DeleteModelVariantId, 1),
                new OrderItem(UpdateOrderItemId, UpdateOrderId, UpdateModelVariantId, 2)
            );
            dbContext.Reviews.AddRange(
                new Review(DeleteReviewId, DeleteModelId, DeleteUserId, DateTime.Now, 1, "test commentary 1"),
                new Review(UpdateReviewId, UpdateModelId, UpdateUserId, DateTime.Now, 5, "test commentary 2")
            );
            dbContext.SaveChanges();
        }
    }
}
