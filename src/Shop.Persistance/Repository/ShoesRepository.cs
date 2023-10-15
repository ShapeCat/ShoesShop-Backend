using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class ShoesRepository : IShoesRepository
    {
        private readonly ShopDbContext dbContext;

        public ShoesRepository(ShopDbContext dbContext) => this.dbContext = dbContext;

        public async Task<IEnumerable<Shoes>> GetAllAsync(CancellationToken cancellationToken)
        {
            var allShoes = await dbContext.Shoes.Include(x => x.Description)
                                                .Include(x => x.Sizes)
                                                .ToListAsync(cancellationToken);
            return allShoes;
        }

        public async Task<bool> ExistsAsync(Guid shoesId, CancellationToken cancellationToken)
        {
            var isExists = await dbContext.Shoes.AnyAsync(x => x.Id == shoesId, cancellationToken);
            return isExists;
        }

        public async Task<Shoes> GetAsync(Guid shoesId, CancellationToken cancellationToken)
        {
            var shoes = await dbContext.Shoes.Include(x => x.Description)
                                             .Include(x => x.Sizes)
                                             .FirstOrDefaultAsync(x => x.Id == shoesId, cancellationToken)
                        ?? throw new NotFoundException(shoesId.ToString(), typeof(Shoes));
            return shoes;
        }

        public async Task<Shoes> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var shoes = await dbContext.Shoes.Include(x => x.Description)
                                             .Include(x => x.Sizes)
                                             .FirstOrDefaultAsync(x => x.Name == name, cancellationToken)
                        ?? throw new NotFoundException(name, typeof(Shoes));
            return shoes;
        }

        public async Task<bool> HasDescriptionAsync(Guid shoesId, CancellationToken cancellationToken)
        {
            var hasDescription = await dbContext.Descriptions.AnyAsync(x => x.ShoesId == shoesId, cancellationToken);
            return hasDescription;
        }

        public Task<int> CountSizesAsync(Guid shoesId, CancellationToken cancellationToken)
        {
            var sizesCount = dbContext.Sizes.Where(x => x.ShoesId == shoesId)
                                            .CountAsync(cancellationToken);
            return sizesCount;
        }

        public async Task<Description> GetShoesDescriptionAsync(Guid shoesId, CancellationToken cancellationToken)
        {
            var modelDescription = await dbContext.Descriptions.FirstOrDefaultAsync(x => x.ShoesId == shoesId, cancellationToken)
                                   ?? throw new NotFoundException(shoesId.ToString(), typeof(Description));
            return modelDescription;
        }

        public async Task<IEnumerable<ShoesSize>> GetShoesSizesAsync(Guid shoesId, CancellationToken cancellationToken)
        {
            var shoesSizes = await dbContext.Sizes.Where(x => x.Id == shoesId)
                                                  .ToListAsync(cancellationToken);
            return shoesSizes;
        }

        public async Task AddAsync(Shoes item, CancellationToken cancellationToken)
        {
            await dbContext.Shoes.AddAsync(item, cancellationToken);
        }

        public async Task RemoveAsync(Guid shoesId, CancellationToken cancellationToken)
        {
            var shoes = await dbContext.Shoes.FirstOrDefaultAsync(x => x.Id == shoesId, cancellationToken)
                        ?? throw new NotFoundException(shoesId.ToString(), typeof(Shoes));
            dbContext.Shoes.Remove(shoes);
        }

        public async Task EditNameAsync(Guid shoesId, string newName, CancellationToken cancellationToken)
        {
            var shoes = await dbContext.Shoes.FirstOrDefaultAsync(x => x.Id == shoesId, cancellationToken)
                        ?? throw new NotFoundException(shoesId.ToString(), typeof(Shoes));
            shoes.Name = newName;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
