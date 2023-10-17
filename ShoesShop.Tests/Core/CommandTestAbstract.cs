using ShoesShop.Persistence;

namespace ShoesShop.Tests.Core
{
    public abstract class CommandTestAbstract : IDisposable
    {
        protected readonly ShopDbContext dbContext;

        public CommandTestAbstract()
        {
            dbContext = ShoesShopTextContext.Create();
        }
        public void Dispose() => ShoesShopTextContext.Destroy(dbContext);
    }
}
