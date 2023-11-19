using AutoMapper;
using ShoesShop.Application.Requests.Adresses.OutputVMs;
using ShoesShop.Application.Requests.ModelsVariants.OutputVMs;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
//using ShoesShop.Application.Requests.Prices.OutputVMs;
using ShoesShop.Persistence;
using Xunit;
using ShoesShop.Application.Requests.Sales.OutputVMs;

namespace ShoesShop.Tests.Core
{
    public class QueryFixture : IDisposable
    {
        public ShopDbContext DbContext { get; }
        public IMapper Mapper { get; }

        public QueryFixture()
        {
            DbContext = ShoesShopTestContext.Create();
            Mapper = new MapperConfiguration(x => x.AddProfiles(new Profile[]
            {
                new AddressVmProfiles(),
                new ImageVmProfiles(),
                new ModelVmProfiles(),
                new ModelSizeVmProfiles(),
                new ModelVariantVmProfiles(),
                //new PriceVmProfiles(),
                new SaleVmProfiles(),
            })).CreateMapper();
        }

        public void Dispose() => ShoesShopTestContext.Destroy(DbContext);
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryFixture> { }
}
