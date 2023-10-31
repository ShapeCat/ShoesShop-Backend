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
