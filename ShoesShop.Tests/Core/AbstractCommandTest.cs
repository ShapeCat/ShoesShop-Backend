using ShoesShop.Application.Interfaces;
using ShoesShop.Persistence;

namespace ShoesShop.Tests.Core
{
    public abstract class AbstractCommandTest : IDisposable
    {
        protected readonly ShopDbContext DbContext;
        protected readonly IUnitOfWork UnitOfWork;

        public AbstractCommandTest()
        {
            DbContext = ShoesShopTestContext.Create();
            UnitOfWork = new UnitOfWork(DbContext, false);
        }

        public void Dispose() => ShoesShopTestContext.Destroy(DbContext);
    }
}
