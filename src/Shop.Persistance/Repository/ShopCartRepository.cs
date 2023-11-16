using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class ShopcartRepository : GenericRepository<Shopcart>
    {
        public ShopcartRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(Shopcart item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<Shopcart>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<Shopcart> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.ShopcartId == Id, cancellationToken)
                ?? throw new NotFoundException(Id.ToString(), typeof(Shopcart));
        }

        public override async Task<IEnumerable<Shopcart>> FindAllAsync(Expression<Func<Shopcart, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }
    }
}
