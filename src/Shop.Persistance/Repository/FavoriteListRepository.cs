﻿using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class FavoriteListRepository : GenericRepository<FavoritesList>
    {
        public FavoriteListRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(FavoritesList item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<FavoritesList>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<FavoritesList> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                ?? throw new NotFoundException(Id.ToString(), typeof(FavoritesList));
        }

        public override async Task<IEnumerable<FavoritesList>> FindAllAsync(Expression<Func<FavoritesList, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }
    }
}