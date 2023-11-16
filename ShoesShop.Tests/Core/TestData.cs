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
                using SHA256 sHA256 = SHA256.Create();
                var Password = Guid.NewGuid().ToString();
                return sHA256.ComputeHash(Encoding.UTF8.GetBytes("test password 1"));
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
        public static Guid DeleteReviewId { get; } = Guid.NewGuid();
        public static Guid UpdateReviewId { get; } = Guid.NewGuid();
        public static Guid DeleteUserId { get; } = Guid.NewGuid();
        public static Guid UpdateUserId { get; } = Guid.NewGuid();
        public static int ExistedModelSize { get; } = 44;

        public static void SeedData(ShopDbContext dbContext)
        {
            var modelSizes = new List<ModelSize>()
            {
                new ModelSize()
                {
                    Id = DeleteModelSizeId,
                    Size = 1,
                },
                new ModelSize()
                {
                    Id = UpdateModelSizeId,
                    Size = ExistedModelSize,
                },
            };
            var images = new List<Image>()
            {
                new Image()
                {
                    Id = DeleteImageId,
                    Url = "test url 1",
                    IsPreview = true,
                },
                new Image()
                {
                    Id = UpdateImageId,
                    Url = "test url 2",
                    IsPreview = false,
                },
            };
            var models = new List<Model>()
            {
                new Model()
                {
                    Id = DeleteModelId,
                    Name = "test name 1",
                    Color = "test color 1",
                    Brend = "test brend 1",
                    SkuId = "test skuid 1",
                    ReleaseDate = DateTime.Now,
                    Images = new List<Image>(images),
                },
                new Model()
                {
                    Id = UpdateModelId,
                    Name = "test name 2",
                    Color = "test color 2",
                    Brend = "test brend 2",
                    SkuId = "test skuid 2",
                    ReleaseDate = DateTime.Now,
                    Images = new List<Image>(){images[1]},
                },
            };
            var prices = new List<Price>()
            {
                new Price()
                {
                    Id = DeletePriceId,
                    BasePrice = 1000,
                },
                 new Price()
                 {
                     Id = UpdatePriceId,
                     BasePrice = 1000,
                     Sale = 1,
                     SaleEndDate = DateTime.Now,
                 }
            };
            var modelVariants = new List<ModelVariant>()
            {
                new ModelVariant()
                {
                    Id = DeleteModelVariantId,
                    Model = models[0],
                    ModelSize = modelSizes[0],
                    Price = prices[0],
                    ItemsLeft = 0,
                },
                new ModelVariant()
                {
                    Id = UpdateModelVariantId,
                    Model = models[1],
                    ModelSize = modelSizes[1],
                    Price = prices[1],
                    ItemsLeft = 1,
                }
            };
            var favoriteItems = new List<FavoritesItem>()
            {
                new FavoritesItem()
                {
                    Id = DeleteFavoriteItemId,
                    ModelVariant = modelVariants[0],
                },
                new FavoritesItem()
                {
                    Id = UpdateFavoriteItemId,
                    ModelVariant = modelVariants[1],
                },
            };
            var favoriteLists = new List<FavoritesList>()
            {
                new FavoritesList()
                {
                    Id = Guid.NewGuid(),
                    Items = new List<FavoritesItem>()
                    {
                        favoriteItems[0]
                    },
                },
                new FavoritesList()
                {
                    Id= Guid.NewGuid(),
                    Items = new List<FavoritesItem>(favoriteItems)
                },
            };
            var cartItems = new List<CartItem>()
            {
                new CartItem()
                {
                    Id = DeleteCartItemId,
                    ModelVariant = modelVariants[0],
                    Amount = 0,
                },
                new CartItem()
                {
                    Id = UpdateCartItemId,
                    ModelVariant = modelVariants[1],
                    Amount = 1,
                }
            };
            var shopCarts = new List<ShopCart>()
            {
                new ShopCart()
                {
                    Id = Guid.NewGuid(),
                    Items = new List<CartItem>
                    {
                        cartItems[0]
                    },
                },
                new ShopCart
                {
                    Id = Guid.NewGuid(),
                    Items = new List<CartItem>(cartItems),
                }
            };
            var orderItems = new List<OrderItem>()
            {
                new OrderItem()
                {
                    Id = Guid.NewGuid(),
                    ModelVariant = modelVariants[0],
                    Amount= 0,
                },
                new OrderItem()
                {
                    Id = Guid.NewGuid(),
                    ModelVariant = modelVariants[1],
                    Amount = 1,
                },
            };
            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = DeleteOrderId,
                    Status = OrderStatus.InProcess,
                    CreationDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        orderItems[0]
                    },
                },
                new Order()
                {
                    Id = UpdateOrderId,
                    Status = OrderStatus.Finished,
                    CreationDate = DateTime.Now,
                    Items = new List<OrderItem>(orderItems),
                },
            };
            var reviews = new List<Review>()
            {
                new Review()
                {
                    Id = DeleteReviewId,
                    Comment = "test commentary 1",
                    PublishDate = DateTime.Now,
                    Model = models[0],
                    Rating = 1,
                },
                new Review()
                {
                    Id = UpdateReviewId,
                    Comment = "test commentary 2",
                    PublishDate = DateTime.Now,
                    Model = models[1],
                    Rating = 5,
                }
            };
            var addresses = new List<Address>()
             {
                new Address()
                {
                    Id = DeleteAddressId,
                    Country = "test country 1",
                    City = "test city 1",
                    Street = "test street 1",
                    House = "444",
                    Room = 1,
                },
                new Address()
                {
                    Id = UpdateAddressId,
                    Country = "test country 2",
                    City = "test city 2",
                    Street = "test street 2",
                    House = "444",
                },
            };
            var users = new List<User>()
            {
                new User()
                {
                    Id = DeleteUserId,
                    Address = addresses[0],
                    UserName = "test user 1",
                    Login = "test login 1",
                    Password = RandomPasswordHash,
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
                    Rewiews = new List<Review>()
                    {
                        reviews[0]
                    },
                },
                new User()
                {
                    Id = UpdateUserId,
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
                    Rewiews = new List<Review>(reviews),
                }
            };
            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();
        }
    }
}
