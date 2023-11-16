using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class PriceRepository : GenericRepository<Price>
    {
        public PriceRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<Price>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<Price> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(Price));
        }

        public override async Task<IEnumerable<Price>> FindAllAsync(Expression<Func<Price, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task EditAsync(Price newItem, CancellationToken cancellationToken)
        {
            var price = await dbSet.FirstOrDefaultAsync(x => x.Id == newItem.Id, cancellationToken)
                        ?? throw new NotFoundException(newItem.Id.ToString(), typeof(Price));
            (price.BasePrice, price.Sale, price.SaleEndDate)
                = (newItem.BasePrice, newItem.Sale, newItem.SaleEndDate);
        }
    }
}
