using ShoesShop.Application.Interfaces;
using ShoesShop.Persistence;

namespace ShoesShop.Tests.Core
{
    public abstract class CommandTestAbstract : IDisposable
    {
        protected readonly ShopDbContext dbContext;
        protected readonly IUnitOfWork unitOfWork;

        public CommandTestAbstract()
        {
            dbContext = ShoesShopTextContext.Create();
            unitOfWork = new UnitOfWork(dbContext);
        }
        public void Dispose() => ShoesShopTextContext.Destroy(dbContext);
    }
}
