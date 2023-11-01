using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class ShopCartRepository : GenericRepository<ShopCart>
    {
        public ShopCartRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<ShopCart>> FindAllAsync(Expression<Func<ShopCart, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Include(x => x.Items)
                              .Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task<IEnumerable<ShopCart>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.Include(x => x.Items)
                              .ToListAsync(cancellationToken);
        }

        public override async Task<ShopCart> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.Include(x => x.Items)
                              .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                ?? throw new NotFoundException(Id.ToString(), typeof(ShopCart));
        }
    }
}
