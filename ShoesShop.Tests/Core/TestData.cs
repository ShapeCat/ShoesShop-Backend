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
        public static Guid DeleteSaleId { get; } = Guid.NewGuid();
        public static Guid UpdateSaleId { get; } = Guid.NewGuid();
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
                    ModelSizeId = DeleteModelSizeId,
                    Size = 1,
                },
                new ModelSize()
                {
                    ModelSizeId = UpdateModelSizeId,
                    Size = ExistedModelSize,
                },
            };
            var images = new List<Image>()
            {
                new Image()
                {
                    ImageId = DeleteImageId,
                    Url = "test url 1",
                    IsPreview = true,
                },
                new Image()
                {
                    ImageId = UpdateImageId,
                    Url = "test url 2",
                    IsPreview = false,
                },
            };
            var models = new List<Model>()
            {
                new Model()
                {
                    ModelId = DeleteModelId,
                    Name = "test name 1",
                    Color = "test color 1",
                    Brend = "test brend 1",
                    SkuId = "test skuid 1",
                    ReleaseDate = DateTime.Now,
                    Images = new List<Image>(images),
                },
                new Model()
                {
                    ModelId = UpdateModelId,
                    Name = "test name 2",
                    Color = "test color 2",
                    Brend = "test brend 2",
                    SkuId = "test skuid 2",
                    ReleaseDate = DateTime.Now,
                    Images = new List<Image>(){images[1]},
                },
            };
            //var prices = new List<Price>()
            //{
            //    new Price()
            //    {
            //        PriceId = DeletePriceId,
            //        BasePrice = 1000,
            //    },
            //     new Price()
            //     {
            //         PriceId = UpdatePriceId,
            //         BasePrice = 1000,
            //     }
            //};
            var sales = new List<Sale>()
            {
            new Sale()
            {
                SaleId = DeleteSaleId,
                Percent = 1,
                SaleEndDate = DateTime.Now,
            },
            new Sale()
            {
                SaleId = UpdateSaleId,
                Percent = 2,
                SaleEndDate = DateTime.Now,
            }
            };
            var modelVariants = new List<ModelVariant>()
            {
                new ModelVariant()
                {
                    ModelVariantId = DeleteModelVariantId,
                    Model = models[0],
                    ModelSize = modelSizes[0],
                    //Price = prices[0],
                    ItemsLeft = 0,
                },
                new ModelVariant()
                {
                    ModelVariantId = UpdateModelVariantId,
                    Model = models[1],
                    ModelSize = modelSizes[1],
                    //Price = prices[1],
                    ItemsLeft = 1,
                }
            };
            var favoriteItems = new List<FavoritesItem>()
            {
                new FavoritesItem()
                {
                    FavoriteItemId = DeleteFavoriteItemId,
                    ModelVariant = modelVariants[0],
                },
                new FavoritesItem()
                {
                    FavoriteItemId = UpdateFavoriteItemId,
                    ModelVariant = modelVariants[1],
                },
            };
            var favoriteLists = new List<FavoritesList>()
            {
                new FavoritesList()
                {
                    FavoriteListId = Guid.NewGuid(),
                    Items = new List<FavoritesItem>()
                    {
                        favoriteItems[0]
                    },
                },
                new FavoritesList()
                {
                    FavoriteListId= Guid.NewGuid(),
                    Items = new List<FavoritesItem>(favoriteItems)
                },
            };
            var cartItems = new List<ShopcartItem>()
            {
                new ShopcartItem()
                {
                    ShopcartItemId = DeleteCartItemId,
                    ModelVariant = modelVariants[0],
                    Amount = 0,
                },
                new ShopcartItem()
                {
                    ShopcartItemId = UpdateCartItemId,
                    ModelVariant = modelVariants[1],
                    Amount = 1,
                }
            };
            var shopCarts = new List<Shopcart>()
            {
                new Shopcart()
                {
                    ShopcartId = Guid.NewGuid(),
                    Items = new List<ShopcartItem>
                    {
                        cartItems[0]
                    },
                },
                new Shopcart
                {
                    ShopcartId = Guid.NewGuid(),
                    Items = new List<ShopcartItem>(cartItems),
                }
            };
            var orderItems = new List<OrderItem>()
            {
                new OrderItem()
                {
                    OrderItemId = Guid.NewGuid(),
                    ModelVariant = modelVariants[0],
                    Amount= 0,
                },
                new OrderItem()
                {
                    OrderItemId = Guid.NewGuid(),
                    ModelVariant = modelVariants[1],
                    Amount = 1,
                },
            };
            var orders = new List<Order>()
            {
                new Order()
                {
                    OrderId = DeleteOrderId,
                    Status = OrderStatus.InProcess,
                    CreationDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        orderItems[0]
                    },
                },
                new Order()
                {
                    OrderId = UpdateOrderId,
                    Status = OrderStatus.Finished,
                    CreationDate = DateTime.Now,
                    Items = new List<OrderItem>(orderItems),
                },
            };
            var reviews = new List<Review>()
            {
                new Review()
                {
                    ReviewId = DeleteReviewId,
                    Comment = "test commentary 1",
                    PublishDate = DateTime.Now,
                    Model = models[0],
                    Rating = 1,
                },
                new Review()
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
                new Address()
                {
                    AddressId = DeleteAddressId,
                    Country = "test country 1",
                    City = "test city 1",
                    Street = "test street 1",
                    House = "444",
                    Room = 1,
                },
                new Address()
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
                new User()
                {
                    UserId = DeleteUserId,
                    Address = addresses[0],
                    UserName = "test user 1",
                    Login = "test login 1",
                    Password = RandomPasswordHash,
                    Phone = "test phone 1",
                    Favorites = favoriteLists[0],
                    Shopcarts = new List<Shopcart>()
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
                    UserId = UpdateUserId,
                    Address = addresses[1],
                    UserName = "test user 2",
                    Login = "test login 2",
                    Password = RandomPasswordHash,
                    Phone = "test phone 2",
                    Favorites = favoriteLists[1],
                    Shopcarts = new List<Shopcart>()
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
