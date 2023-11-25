using System.Security.Cryptography;
using System.Text;
using ShoesShop.Entities;
using ShoesShop.Persistence;

namespace ShoesShop.Tests.Core
{
    internal static class TestData
    {
        private static byte[] RandomPasswordHash
        {
            get
            {
                using var sHA256 = SHA256.Create();
                var Password = Guid.NewGuid().ToString();
                return User.HashPassword(Password);
            }
        }

        public static Guid DeleteAddressId { get; } = Guid.NewGuid();
        public static Guid UpdateAddressId { get; } = Guid.NewGuid();
        public static Guid DeleteCartItemId { get; } = Guid.NewGuid();
        public static Guid UpdateCartItemId { get; } = Guid.NewGuid();
        public static Guid DeleteFavoriteItemId { get; } = Guid.NewGuid();
        public static Guid UpdateFavoriteItemId { get; } = Guid.NewGuid();
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
        public static Guid DeletePriceId { get; } = Guid.NewGuid();
        public static Guid UpdatePriceId { get; } = Guid.NewGuid();
        public static Guid DeleteSaleId { get; } = Guid.NewGuid();
        public static Guid UpdateSaleId { get; } = Guid.NewGuid();
        public static Guid DeleteReviewId { get; } = Guid.NewGuid();
        public static Guid UpdateReviewId { get; } = Guid.NewGuid();
        public static Guid DeleteUserId { get; } = Guid.NewGuid();
        public static Guid UpdateUserId { get; } = Guid.NewGuid();
        public static int ExistedModelSize { get; } = 44;
        public static (string login, string password) ExistedLoginData = ("login1234565432", "pass1234567");

        public static void SeedData(ShopDbContext dbContext)
        {
            var modelSizes = new List<ModelSize>()
            {
                new ()
                {
                    ModelSizeId = DeleteModelSizeId,
                    Size = 1,
                },
                new ()
                {
                    ModelSizeId = UpdateModelSizeId,
                    Size = ExistedModelSize,
                },
            };
            var images = new List<Image>()
            {
                new ()
                {
                    ImageId = DeleteImageId,
                    Url = "test url 1",
                    IsPreview = true,
                },
                new ()
                {
                    ImageId = UpdateImageId,
                    Url = "test url 2",
                    IsPreview = false,
                },
            };
            var models = new List<Model>()
            {
                new ()
                {
                    ModelId = DeleteModelId,
                    Name = "test name 1",
                    Color = "test color 1",
                    Brand = "test brand 1",
                    SkuId = "test SkuId 1",
                    ReleaseDate = DateTime.Now,
                    Images = new List<Image>(images),
                },
                new ()
                {
                    ModelId = UpdateModelId,
                    Name = "test name 2",
                    Color = "test color 2",
                    Brand = "test brand 2",
                    SkuId = "test SkuId 2",
                    ReleaseDate = DateTime.Now,
                    Images = new List<Image>()
                    {
                        images[1]
                    },
                },
            };
            var sales = new List<Sale>()
            {
                new ()
                {
                    SaleId = DeleteSaleId,
                    Percent = 1,
                    SaleEndDate = DateTime.Now,
                },
                new ()
                {
                    SaleId = UpdateSaleId,
                    Percent = 2,
                    SaleEndDate = DateTime.Now,
                }
            };
            var modelVariants = new List<ModelVariant>()
            {
                new ()
                {
                    ModelVariantId = DeleteModelVariantId,
                    Model = models[0],
                    ModelSize = modelSizes[0],
                    Sales = new List<Sale>()
                    {
                        sales[0]
                    },
                    ItemsLeft = 0,
                },
                new ()
                {
                    ModelVariantId = UpdateModelVariantId,
                    Model = models[1],
                    ModelSize = modelSizes[1],
                    Sales = new List<Sale>()
                    {
                        sales[1]
                    },
                    ItemsLeft = 1,
                }
            };
            var favoriteItems = new List<FavoritesItem>()
            {
                new ()
                {
                    FavoriteItemId = DeleteFavoriteItemId,
                    ModelVariant = modelVariants[0],
                },
                new ()
                {
                    FavoriteItemId = UpdateFavoriteItemId,
                    ModelVariant = modelVariants[1],
                },
            };
            var favoriteLists = new List<FavoritesList>()
            {
                new ()
                {
                    FavoriteListId = Guid.NewGuid(),
                    Items = new List<FavoritesItem>()
                    {
                        favoriteItems[0]
                    },
                },
                new ()
                {
                    FavoriteListId= Guid.NewGuid(),
                    Items = new List<FavoritesItem>(favoriteItems)
                },
            };
            var cartItems = new List<ShopCartItem>()
            {
                new ()
                {
                    ShopCartItemId = DeleteCartItemId,
                    ModelVariant = modelVariants[0],
                    Amount = 0,
                },
                new ()
                {
                    ShopCartItemId = UpdateCartItemId,
                    ModelVariant = modelVariants[1],
                    Amount = 1,
                }
            };
            var shopCarts = new List<ShopCart>()
            {
                new ()
                {
                    ShopCartId = Guid.NewGuid(),
                    Items = new List<ShopCartItem>
                    {
                        cartItems[0]
                    },
                },
                new ()
                {
                    ShopCartId = Guid.NewGuid(),
                    Items = new List<ShopCartItem>(cartItems),
                }
            };
            var orderItems = new List<OrderItem>()
            {
                new ()
                {
                    OrderItemId = Guid.NewGuid(),
                    ModelVariant = modelVariants[0],
                    Amount= 0,
                },
                new ()
                {
                    OrderItemId = Guid.NewGuid(),
                    ModelVariant = modelVariants[1],
                    Amount = 1,
                },
            };
            var orders = new List<Order>()
            {
                new ()
                {
                    OrderId = DeleteOrderId,
                    Status = OrderStatus.InProcess,
                    CreationDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        orderItems[0]
                    },
                },
                new ()
                {
                    OrderId = UpdateOrderId,
                    Status = OrderStatus.Finished,
                    CreationDate = DateTime.Now,
                    Items = new List<OrderItem>(orderItems),
                },
            };
            var reviews = new List<Review>()
            {
                new ()
                {
                    ReviewId = DeleteReviewId,
                    Comment = "test commentary 1",
                    PublishDate = DateTime.Now,
                    Model = models[0],
                    Rating = 1,
                },
                new ()
                {
                    ReviewId = UpdateReviewId,
                    Comment = "test commentary 2",
                    PublishDate = DateTime.Now,
                    Model = models[1],
                    Rating = 5,
                }
            };
            var addresses = new List<Address>()
             {
                new ()
                {
                    AddressId = DeleteAddressId,
                    Country = "test country 1",
                    City = "test city 1",
                    Street = "test street 1",
                    House = "444",
                    Room = 1,
                },
                new ()
                {
                    AddressId = UpdateAddressId,
                    Country = "test country 2",
                    City = "test city 2",
                    Street = "test street 2",
                    House = "444",
                },
            };
            var users = new List<User>()
            {
                new ()
                {
                    UserId = DeleteUserId,
                    Address = addresses[0],
                    UserName = "test user 1",
                    Login = ExistedLoginData.login,
                    Password = User.HashPassword(ExistedLoginData.password),
                    Phone = "test phone 1",
                    Favorites = favoriteLists[0],
                    ShopCarts = new List<ShopCart>()
                    {
                        shopCarts[0],
                    },
                    Orders = new List<Order>()
                    {
                        orders[0]
                    },
                    Reviews = new List<Review>()
                    {
                        reviews[0]
                    },
                },
                new ()
                {
                    UserId = UpdateUserId,
                    Address = addresses[1],
                    UserName = "test user 2",
                    Login = "test login 2",
                    Password = RandomPasswordHash,
                    Phone = "test phone 2",
                    Favorites = favoriteLists[1],
                    ShopCarts = new List<ShopCart>()
                    {
                        shopCarts[1],
                    },
                    Orders = new List<Order>()
                    {
                        orders[1]
                    },
                    Reviews = new List<Review>(reviews),
                }
            };
            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();
        }
    }
}
