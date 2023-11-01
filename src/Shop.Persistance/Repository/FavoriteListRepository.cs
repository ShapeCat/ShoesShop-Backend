using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class FavoriteListRepository : GenericRepository<FavoritesList>
    {
        public FavoriteListRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<FavoritesList>> FindAllAsync(Expression<Func<FavoritesList, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Include(x => x.Items)
                              .Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task<IEnumerable<FavoritesList>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.Include(x => x.Items)
                              .ToListAsync(cancellationToken);
        }

        public override async Task<FavoritesList> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.Include(x => x.Items)
                              .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                ?? throw new NotFoundException(Id.ToString(), typeof(FavoritesList));
        }
    }
}
