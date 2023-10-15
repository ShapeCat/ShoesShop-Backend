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

        public async Task<IEnumerable<ShoesSize>> GetAllAsync(CancellationToken cancellationToken)
        {
            var allSizeInfo = await dbContext.Sizes.ToListAsync(cancellationToken);
            return allSizeInfo;
        }

        public async Task<bool> ExistsAsync(Guid shoesSizeId, CancellationToken cancellationToken)
        {
            var isExists = await dbContext.Sizes.AnyAsync(x => x.Id == shoesSizeId, cancellationToken);
            return isExists;
        }

        public async Task<bool> ExistsAsync(Guid shoesId, int size, CancellationToken cancellationToken)
        {
            var isExists = await dbContext.Sizes.AnyAsync(x => x.ShoesId == shoesId && x.Size == size, cancellationToken);
            return isExists;
        }

        public async Task<ShoesSize> GetAsync(Guid shoesSizeId, CancellationToken cancellationToken)
        {
            var sizeInfo = await dbContext.Sizes.FirstOrDefaultAsync(x => x.Id == shoesSizeId, cancellationToken)
                       ?? throw new NotFoundException(shoesSizeId.ToString(), typeof(Shoes));
            return sizeInfo;
        }

        public async Task<IEnumerable<ShoesSize>> GetByShoesAsync(Guid shoesId, CancellationToken cancellationToken)
        {
            var sizeInfo = await dbContext.Sizes.Where(x => x.ShoesId == shoesId)
                                            .ToListAsync(cancellationToken)
                       ?? throw new NotFoundException(shoesId.ToString(), typeof(Shoes));
            return sizeInfo;
        }

        public async Task<ShoesSize> GetByShoesAsync(Guid shoesId, int size, CancellationToken cancellationToken)
        {
            var result = await dbContext.Sizes.FirstOrDefaultAsync(x => x.ShoesId == shoesId && x.Size == size)
                        ?? throw new NotFoundException(shoesId.ToString(), typeof(Shoes));
            return result;
        }

        public async Task AddAsync(Guid shoesId, ShoesSize item, CancellationToken cancellationToken)
        {
            var shoes = await dbContext.Shoes.Include(x => x.Sizes)
                                             .FirstOrDefaultAsync(x => x.Id == shoesId, cancellationToken)
                        ?? throw new NotFoundException(shoesId.ToString(), typeof(Shoes));
            shoes.Sizes ??= new List<ShoesSize>();
            if (shoes.Sizes.Any(x => x.Size == item.Size))
            {
                throw new AlreadyExistsException(item.Size.ToString(), typeof(ShoesSize));
            }
            shoes.Sizes.Add(item);
        }

        public async Task RemoveAsync(Guid shoesSizeId, CancellationToken cancellationToken)
        {
            var sizeInfo = await dbContext.Sizes.FirstOrDefaultAsync(x => x.Id == shoesSizeId, cancellationToken)
                        ?? throw new NotFoundException(shoesSizeId.ToString(), typeof(ShoesSize));
            dbContext.Sizes.Remove(sizeInfo);
        }

        public async Task EditAsync(Guid shoesSizeId, ShoesSize item, CancellationToken cancellationToken)
        {
            var sizeInfo = await dbContext.Sizes.FirstOrDefaultAsync(x => x.Id == shoesSizeId, cancellationToken)
                           ?? throw new NotFoundException(shoesSizeId.ToString(), typeof(ShoesSize));
            (sizeInfo.Size, sizeInfo.Price, sizeInfo.ItemsLeft) = (item.Size, item.Price, item.ItemsLeft);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
