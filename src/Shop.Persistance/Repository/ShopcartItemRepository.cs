using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ShoesShop.Entities;
using ShoesShop.Application.Common.Exceptions;

namespace ShoesShop.Persistence.Repository
{
    public class ShopcartItemRepository : GenericRepository<ShopcartItem>
    {
        public ShopcartItemRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<ShopcartItem>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<ShopcartItem> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(ShopcartItem));
        }

        public override async Task<IEnumerable<ShopcartItem>> FindAllAsync(Expression<Func<ShopcartItem, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task EditAsync(ShopcartItem newItem, CancellationToken cancellationToken)
        {
            var shopcartItem = await dbSet.FirstOrDefaultAsync(x => x.Id == newItem.Id, cancellationToken)
                           ?? throw new NotFoundException(newItem.Id.ToString(), typeof(ShopcartItem));
            (shopcartItem.Amount)
                = (newItem.Amount);
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var shopcartItem = await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                           ?? throw new NotFoundException(Id.ToString(), typeof(ShopcartItem));
            dbSet.Remove(shopcartItem);
        }
    }
}
