using Microsoft.EntityFrameworkCore.Infrastructure;
using ShoesShop.Application.Interfaces;
using ShoesShop.Entities;
using ShoesShop.Persistence.Repository;

namespace ShoesShop.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private Dictionary<Type, object> customRepositories;

        public ShopDbContext dbContext { get; }

        public UnitOfWork(ShopDbContext context)
        {
            dbContext = context ?? throw new ArgumentNullException(nameof(context));
            customRepositories = new Dictionary<Type, object>
            {

                { typeof(Adress), new AdressRepository(dbContext) },
                { typeof(CartItem), new CartItemRepository(dbContext) },
                { typeof(FavoritesItem), new FavoritesItemRepository(dbContext) },
                { typeof(FavoritesList), new FavoriteListRepository(dbContext) },
                { typeof(Image), new ImageRepository(dbContext) },
                { typeof(Model), new ModelRepository(dbContext) },
                { typeof(ModelSize), new ModelSizeRepository(dbContext) },
                { typeof(ModelVariant), new ModelVariantRepository(dbContext) },
                { typeof(Order), new OrderRepository(dbContext) },
                { typeof(OrderItem), new OrderItemRepository(dbContext) },
                { typeof(Review), new ReviewRepository(dbContext) },
                { typeof(ShopCart), new ShopCartRepository(dbContext) },
                { typeof(User), new UserRepository(dbContext) },
            };
        }

        public IRepositoryOf<T> GetRepositoryOf<T>(bool hasCustomRepository = false) where T : class
        {
            var entityType = typeof(T);
            if (hasCustomRepository)
            {
                try
                {
                    var customRepository = dbContext.GetService<IRepositoryOf<T>>();
                    return customRepository;

                }
                catch (InvalidOperationException)
                {
                    // For Unit-Tests or service exception
                    if (customRepositories.ContainsKey(entityType))
                    {
                        return customRepositories[entityType] as IRepositoryOf<T>;
                    }
                }
            }
            return new GenericRepository<T>(dbContext);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (!_disposed || disposing)
            {
                customRepositories?.Clear();
                dbContext?.Dispose();
            }
            _disposed = true;
        }
    }
}
