using Microsoft.EntityFrameworkCore;
using ShoesShop.Entities;
using ShoesShop.Persistence;

namespace ShoesShop.Tests.Core
{
    internal static class ShoesShopTextContext
    {
        public static Guid EmptyShoes { get; } = Guid.NewGuid();
        public static Guid FullShoes { get; } = Guid.NewGuid();
        public static Guid ShoesToDelete { get; } = Guid.NewGuid();
        public static Guid DescriptionToDelete { get; } = Guid.NewGuid();
        public static Guid DescriptionToUpdate { get; } = Guid.NewGuid();
        public static Guid ShoesSizeToDelete { get; } = Guid.NewGuid();
        public static Guid ShoesSizeToUpdate { get; } = Guid.NewGuid();
        public static int ExistedSize { get; } = 44;
        public static int ItemsCount { get; private set; }

        public static ShopDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ShopDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new ShopDbContext(options);
            dbContext.Database.EnsureCreated();

            var shoesPairs = new List<Shoes>()
                {
                    // Empty shoes to add things
                    new Shoes()
                    {
                        Id = EmptyShoes,
                        Name = "Empty Shoes"
                    },
                    // Shoes with full info to check existance and update 
                    new Shoes()
                    {
                        Id = FullShoes,
                        Name = "Full Shoes",
                        Description = new Description
                        {
                            Id = DescriptionToUpdate,
                            ColorName = "TestColor1",
                            ReleaseDate = new DateTime(2024, 2, 6),
                            SkuID = "TestSkuID_1"
                        },
                        Sizes = new List<ShoesSize>()
                        {
                            new ShoesSize()
                            {
                                Id = ShoesSizeToUpdate,
                                Size = ExistedSize,
                                Price = 1000,
                                ItemsLeft = 100
                            },
                        }
                    },
                    // Shoes to delete
                    new Shoes()
                    {
                        Id = ShoesToDelete,
                        Name = "Test Pair 2",
                    },
                    // Shoes to delete description
                    new Shoes()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test Shoes 3",
                        Description = new Description
                        {
                            Id = DescriptionToDelete,
                            ColorName = "TestColor1",
                            ReleaseDate = new DateTime(2024, 2, 6),
                            SkuID = "TestSkuID_1"
                        },
                    },
                    // Shoes to delete size
                    new Shoes()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test Shoes 4",
                        Sizes = new List<ShoesSize>()
                        {
                            new ShoesSize()
                            {
                                Id = ShoesSizeToDelete,
                                Size = 123,
                                Price = 1000,
                                ItemsLeft = 100
                            },
                        }
                    },
                };

            ItemsCount = shoesPairs.Count;
            dbContext.Shoes.AddRange(shoesPairs);
            dbContext.SaveChanges();
            return dbContext;
        }

        public static void Destroy(ShopDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
