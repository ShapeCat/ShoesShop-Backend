using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Interfaces;
using ShoesShop.Persistence.Exceptions;

namespace ShoesShop.Persistence.Repository
{
    public class GenericRepository<TEntity> : IRepositoryOf<TEntity> where TEntity : class
    {
        protected ShopDbContext dbContext;
        protected DbSet<TEntity> dbSet;

        public GenericRepository(ShopDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public virtual Task AddAsync(TEntity item, CancellationToken cancellationToken)
        {
            throw new ActionNotAllowedException(nameof(AddAsync), typeof(TEntity));
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new ActionNotAllowedException(nameof(GetAllAsync), typeof(TEntity));
        }

        public virtual Task<TEntity> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            throw new ActionNotAllowedException(nameof(GetAsync), typeof(TEntity));
        }

        public virtual Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            throw new ActionNotAllowedException(nameof(FindAllAsync), typeof(TEntity));
        }

        public virtual Task EditAsync(TEntity newItem, CancellationToken cancellationToken)
        {
            throw new ActionNotAllowedException(nameof(EditAsync), typeof(TEntity));
        }

        public virtual Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            throw new ActionNotAllowedException(nameof(RemoveAsync), typeof(TEntity));
        }
    }
}
