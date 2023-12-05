using AutoMapper;
using ShoesShop.Application.Common.Mapping;
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
            Mapper = new MapperConfiguration(cfg => cfg.AddProfiles(VmProfiles.AllProfiles)).CreateMapper();
        }

        public void Dispose() => ShoesShopTestContext.Destroy(DbContext);
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryFixture> { }
}
