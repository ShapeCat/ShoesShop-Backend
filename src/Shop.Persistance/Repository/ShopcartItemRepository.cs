using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class ShopCartItemRepository : GenericRepository<ShopCartItem>
    {
        public ShopCartItemRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(ShopCartItem item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<ShopCartItem>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<ShopCartItem> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(ShopCartItem));
        }

        public override async Task<IEnumerable<ShopCartItem>> FindAllAsync(Expression<Func<ShopCartItem, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task EditAsync(ShopCartItem newItem, CancellationToken cancellationToken)
        {
            var shopCartItem = await dbSet.FirstOrDefaultAsync(x => x.UserId == newItem.UserId, cancellationToken)
                           ?? throw new NotFoundException(newItem.UserId.ToString(), typeof(ShopCartItem));
            shopCartItem.Amount
                = newItem.Amount;
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var shopCartItem = await dbSet.FirstOrDefaultAsync(x => x.UserId == Id, cancellationToken)
                           ?? throw new NotFoundException(Id.ToString(), typeof(ShopCartItem));
            dbSet.Remove(shopCartItem);
        }
    }
}
