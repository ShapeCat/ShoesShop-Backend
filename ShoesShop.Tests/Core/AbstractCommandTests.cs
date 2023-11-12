using ShoesShop.Application.Interfaces;
using ShoesShop.Persistence;

namespace ShoesShop.Tests.Core
{
    public abstract class AbstractCommandTests : IDisposable
    {
        protected ShopDbContext DbContext { get; }
        protected IUnitOfWork UnitOfWork { get; }

        public AbstractCommandTests()
        {
            DbContext = ShoesShopTestContext.Create();
            UnitOfWork = new UnitOfWork(DbContext, false);
        }

        public void Dispose() => ShoesShopTestContext.Destroy(DbContext);
    }
}
