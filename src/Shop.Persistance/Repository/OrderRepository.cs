using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    internal class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(Order item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<Order> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(Order));
        }

        public override async Task<IEnumerable<Order>> FindAllAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task EditAsync(Order newItem, CancellationToken cancellationToken)
        {
            var order = await dbSet.FirstOrDefaultAsync(x => x.Id == newItem.Id, cancellationToken)
                               ?? throw new NotFoundException(newItem.Id.ToString(), typeof(Order));
            (order.Status)
                = (newItem.Status);
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var order = await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                               ?? throw new NotFoundException(Id.ToString(), typeof(Order));
            dbSet.Remove(order);
        }
    }
}
