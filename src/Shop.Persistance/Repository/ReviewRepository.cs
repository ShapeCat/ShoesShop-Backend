using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class ReviewRepository : GenericRepository<Review>
    {
        public ReviewRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(Review item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<Review> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.ReviewId == Id, cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(Review));
        }

        public override async Task<IEnumerable<Review>> FindAllAsync(Expression<Func<Review, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var review = await dbSet.FirstOrDefaultAsync(x => x.ReviewId == Id, cancellationToken)
                               ?? throw new NotFoundException(Id.ToString(), typeof(Review));
            dbSet.Remove(review);
        }
    }
}
