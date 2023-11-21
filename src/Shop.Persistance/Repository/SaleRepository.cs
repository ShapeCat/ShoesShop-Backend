using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class SaleRepository : GenericRepository<Sale>
    {
        public SaleRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(Sale item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<Sale> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.SaleId == Id, cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(Address));
        }

        public override async Task<IEnumerable<Sale>> FindAllAsync(Expression<Func<Sale, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task EditAsync(Sale newItem, CancellationToken cancellationToken)
        {
            var sale = await dbSet.FirstOrDefaultAsync(x => x.SaleId == newItem.SaleId, cancellationToken)
                        ?? throw new NotFoundException(newItem.SaleId.ToString(), typeof(Sale));
            (sale.Percent, sale.SaleEndDate)
                = (newItem.Percent, newItem.SaleEndDate);
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var address = await dbSet.FirstOrDefaultAsync(x => x.SaleId == Id, cancellationToken)
                         ?? throw new NotFoundException(Id.ToString(), typeof(Address)); dbSet.Remove(address);
            dbSet.Remove(address);
        }
    }
}
