using AutoMapper;
using ShoesShop.Application.Requests.Queries.OutputVMs.Profiles;
using ShoesShop.Persistence;
using Xunit;

namespace ShoesShop.Tests.Core
{
    public class QueryFixture : IDisposable
    {
        public ShopDbContext DbContext { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryFixture()
        {
            DbContext = ShoesShopTestContext.Create();
            Mapper = new MapperConfiguration(x => x.AddProfile(new VmProfiles())).CreateMapper();
        }

        public void Dispose() => ShoesShopTestContext.Destroy(DbContext);
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryFixture> { }

}
