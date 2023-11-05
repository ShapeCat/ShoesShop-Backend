using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class ShopCartRepository : GenericRepository<ShopCart>
    {
        public ShopCartRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(ShopCart item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<ShopCart>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<ShopCart> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                ?? throw new NotFoundException(Id.ToString(), typeof(ShopCart));
        }

        public override async Task<IEnumerable<ShopCart>> FindAllAsync(Expression<Func<ShopCart, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

    }
}
