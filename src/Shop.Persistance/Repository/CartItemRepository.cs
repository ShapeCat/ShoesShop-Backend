using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ShoesShop.Entities;
using ShoesShop.Application.Common.Exceptions;

namespace ShoesShop.Persistence.Repository
{
    public class CartItemRepository : GenericRepository<CartItem>
    {
        public CartItemRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<CartItem>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<CartItem> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(CartItem));
        }

        public override async Task<IEnumerable<CartItem>> FindAllAsync(Expression<Func<CartItem, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task EditAsync(CartItem newItem, CancellationToken cancellationToken)
        {
            var cartItem = await dbSet.FirstOrDefaultAsync(x => x.Id == newItem.Id, cancellationToken)
                           ?? throw new NotFoundException(newItem.Id.ToString(), typeof(CartItem));
            (cartItem.Amount)
                = (newItem.Amount);
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var cartItem = await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                           ?? throw new NotFoundException(Id.ToString(), typeof(CartItem));
            dbSet.Remove(cartItem);
        }
    }
}
