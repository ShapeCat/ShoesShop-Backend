using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class ReviewRepository : GenericRepository<Review>
    {
        public ReviewRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<Review>> FindAllAsync(Expression<Func<Review, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Include(x => x.Model)
                              .Include(x => x.Model)
                              .Include(x => x.Author)
                              .Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.Include(x => x.Model)
                              .Include(x => x.Model)
                              .Include(x => x.Author)
                              .ToListAsync(cancellationToken);
        }

        public override async Task<Review> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.Include(x => x.Model)
                              .Include(x => x.Model)
                              .Include(x => x.Author)
                              .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(Review));
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var review = await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                               ?? throw new NotFoundException(Id.ToString(), typeof(Review));
            dbSet.Remove(review);
        }
    }
}
