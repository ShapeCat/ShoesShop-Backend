using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class ShoesSizeRepository : IShoesSizeRepository
    {
        private readonly ShopDbContext dbContext;

        public ShoesSizeRepository(ShopDbContext dbContext) => this.dbContext = dbContext;

        public async Task<IEnumerable<ShoesSize>> FindAllAsync(Expression<Func<ShoesSize, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbContext.Sizes.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ShoesSize>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Sizes.ToListAsync(cancellationToken);
        }

        public async Task<ShoesSize> GetAsync(Guid shoesSizeId, CancellationToken cancellationToken)
        {
            return await dbContext.Sizes.FirstOrDefaultAsync(x => x.Id == shoesSizeId, cancellationToken)
                   ?? throw new NotFoundException(shoesSizeId.ToString(), typeof(Shoes));
        }

        public async Task AddAsync(ShoesSize item, CancellationToken cancellationToken)
        {
            var shoes = await dbContext.Shoes.Where(x => x.Id == item.ShoesId)
                                 .FirstOrDefaultAsync(x => x.Id == item.ShoesId, cancellationToken)
                        ?? throw new NotFoundException(item.ShoesId.ToString(), typeof(Shoes));
            shoes.Sizes ??= new List<ShoesSize>();
            if (shoes.Sizes.Any(x => x.Size == item.Size))
            {
                throw new AlreadyExistsException(item.Size.ToString(), typeof(ShoesSize));
            }
            shoes.Sizes.Add(item);
        }

        public async Task EditAsync(ShoesSize newItem, CancellationToken cancellationToken)
        {
            var sizeToEdit = await dbContext.Sizes.FirstOrDefaultAsync(x => x.Id == newItem.Id)
                             ?? throw new NotFoundException(newItem.Id.ToString(), typeof(ShoesSize));
            if (dbContext.Sizes.Any(x => x.Size == newItem.Size && x.ShoesId == sizeToEdit.ShoesId))
            {
                throw new AlreadyExistsException(newItem.Size.ToString(), typeof(ShoesSize));
            }
            (sizeToEdit.Size, sizeToEdit.Price, sizeToEdit.ItemsLeft) = (newItem.Size, newItem.Price, newItem.ItemsLeft);
        }

        public async Task RemoveAsync(Guid shoesSizeId, CancellationToken cancellationToken)
        {
            var sizeInfo = await dbContext.Sizes.FirstOrDefaultAsync(x => x.Id == shoesSizeId, cancellationToken)
                        ?? throw new NotFoundException(shoesSizeId.ToString(), typeof(ShoesSize));
            dbContext.Sizes.Remove(sizeInfo);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
