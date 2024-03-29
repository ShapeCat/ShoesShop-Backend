﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class FavoritesItemRepository : GenericRepository<FavoritesItem>
    {
        public FavoritesItemRepository(ShopDbContext dbContext) : base(dbContext) { }

        public async override Task AddAsync(FavoritesItem item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<FavoritesItem>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<FavoritesItem> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(FavoritesItem));
        }

        public override async Task<IEnumerable<FavoritesItem>> FindAllAsync(Expression<Func<FavoritesItem, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var shopCartItem = await dbSet.FirstOrDefaultAsync(x => x.FavoriteItemId == Id, cancellationToken)
                           ?? throw new NotFoundException(Id.ToString(), typeof(FavoritesItem));
            dbSet.Remove(shopCartItem);
        }
    }
}
