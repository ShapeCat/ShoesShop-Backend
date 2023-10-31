using ShoesShop.Entities;

namespace ShoesShop.Persistence
{
    public class TestData
    {
        private readonly ShopDbContext dbContext;

        public TestData(ShopDbContext context) => dbContext = context;

        public void SeedDataContext()
        {
            //if (!dbContext.Shoes.Any())
            //{
            //    var shoesPairs = new List<Model>()
            //    {
            //        new Model()
            //        {
            //            Name = "TestPair_1",
            //            Description = new Description
            //            {
            //                ColorName = "Black",
            //                ReleaseDate = new DateTime(2020, 4, 4),
            //                SkuID = "TestSkuID_1"
            //            },
            //            Sizes = new List<ModelSize>()
            //            {
            //                new ModelSize()
            //                {
            //                    Size = 100,
            //                    Price = 1000,
            //                    ItemsLeft = 2
            //                },
            //                new ModelSize()
            //                {
            //                    Size = 200,
            //                    Price = 2000,
            //                    ItemsLeft = 1
            //                },
            //                new ModelSize()
            //                {
            //                    Size = 300,
            //                    Price = 3000
            //                },
            //                new ModelSize()
            //                {
            //                    Size = 400,
            //                    Price = 4000,
            //                    ItemsLeft = 4
            //                }
            //            }
            //        },
            //        new Model()
            //        {
            //            Name = "TestPair_2",
            //            Description = new Description
            //            {
            //                ColorName = "Black",
            //                ReleaseDate = new DateTime(2024, 2, 6),
            //                SkuID = "TestSkuID_2"
            //            },
            //            Sizes = new List<ModelSize>()
            //            {
            //                new ModelSize()
            //                {
            //                    Size = 100,
            //                    Price = 1000,
            //                    ItemsLeft = 234567865
            //                },
            //                new ModelSize()
            //                {
            //                    Size = 200,
            //                    Price = 2000
            //                },
            //                new ModelSize()
            //                {
            //                    Size = 300,
            //                    Price = 3000
            //                },
            //                new ModelSize()
            //                {
            //                    Size = 400,
            //                    Price = 4000
            //                }
            //            }
            //        }
            //    };
            //    dbContext.Shoes.AddRange(shoesPairs);
            //    dbContext.SaveChanges();
            //}
        }
    }
}