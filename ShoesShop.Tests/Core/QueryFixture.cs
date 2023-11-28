using AutoMapper;
using ShoesShop.Application.Requests.Addresses.OutputVMs;
using ShoesShop.Application.Requests.Authentication.OutputVMs;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Application.Requests.ModelsVariants.OutputVMs;
using ShoesShop.Application.Requests.Reviews.OutputVMs;
using ShoesShop.Application.Requests.Sales.OutputVMs;
using ShoesShop.Application.Requests.Users.OutputVMs;
using ShoesShop.Persistence;
using Xunit;
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
                new SaleVmProfiles(),
                new AuthenticationUserProfiles(),
                new UserVmProfiles(),
                new ReviewVmProfile(),
            })).CreateMapper();
        }

        public void Dispose() => ShoesShopTestContext.Destroy(DbContext);
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryFixture> { }
}
