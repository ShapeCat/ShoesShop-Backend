using ShoesShop.Application.Interfaces;
using ShoesShop.Persistence;

namespace ShoesShop.Tests.Core
{
    public abstract class AbstractCommandTests : IDisposable
    {
        protected readonly ShopDbContext DbContext;
        protected readonly IUnitOfWork UnitOfWork;

        public AbstractCommandTests()
        {
            DbContext = ShoesShopTestContext.Create();
            UnitOfWork = new UnitOfWork(DbContext, false);
        }

        public void Dispose() => ShoesShopTestContext.Destroy(DbContext);
    }
}
