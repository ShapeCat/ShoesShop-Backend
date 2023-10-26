using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Interfaces;

namespace ShoesShop.Persistence.Repository
{
    public class GenericRepository<T> : IRepositoryOf<T> where T : class
    {
        protected ShopDbContext dbContext;
        protected DbSet<T> dbSet;

        public GenericRepository(ShopDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public virtual async Task<T> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FindAsync(Id, cancellationToken);
        }

        public virtual async Task AddAsync(T item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public virtual async Task EditAsync(T newItem, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public virtual Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
