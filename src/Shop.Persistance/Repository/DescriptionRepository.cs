using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class DescriptionRepository : IDescriptionRepository
    {
        private readonly ShopDbContext dbContext;

        public DescriptionRepository(ShopDbContext dbContext) => this.dbContext = dbContext;

        public async Task<IEnumerable<Description>> GetAllAsync(CancellationToken cancellationToken)
        {
            var descriptions = await dbContext.Descriptions.ToListAsync(cancellationToken);
            return descriptions;
        }

        public async Task<bool> ExistsAsync(Guid descriptionId, CancellationToken cancellationToken)
        {
            var isExists = await dbContext.Descriptions.AnyAsync(x => x.Id == descriptionId, cancellationToken);
            return isExists;
        }

        public async Task<Description> GetAsync(Guid descriptionId, CancellationToken cancellationToken)
        {
            var description = await dbContext.Descriptions.FirstOrDefaultAsync(x => x.Id == descriptionId, cancellationToken)
                              ?? throw new NotFoundException(descriptionId.ToString(), typeof(Description));
            return description;
        }

        public async Task<Description> GetByShoesAsync(Guid shoesId, CancellationToken cancellationToken)
        {
            var description = await dbContext.Descriptions.FirstOrDefaultAsync(x => x.ShoesId == shoesId, cancellationToken)
                              ?? throw new NotFoundException(shoesId.ToString(), typeof(Shoes));
            return description;
        }

        public async Task AddAsync(Guid shoesId, Description description, CancellationToken cancellationToken)
        {
            var shoes = await dbContext.Shoes.Include(x => x.Description)
                                             .FirstOrDefaultAsync(x => x.Id == shoesId, cancellationToken)
                        ?? throw new NotFoundException(shoesId.ToString(), typeof(Shoes));
            if (shoes.Description is not null) throw new AlreadyExistsException(shoesId.ToString(), typeof(Description));
            shoes.Description = description;
        }

        public async Task RemoveAsync(Guid descriptionId, CancellationToken cancellationToken)
        {
            var description = await dbContext.Descriptions.FirstOrDefaultAsync(x => x.Id == descriptionId, cancellationToken)
                 ?? throw new NotFoundException(descriptionId.ToString(), typeof(Description));
            dbContext.Descriptions.Remove(description);
        }

        public async Task EditAsync(Guid descriptionId, Description newDescription, CancellationToken cancellationToken)
        {
            var description = await dbContext.Descriptions.FirstOrDefaultAsync(x => x.Id == descriptionId, cancellationToken)
                ?? throw new NotFoundException(descriptionId.ToString(), typeof(Description));
            (description.ColorName, description.ReleaseDate, description.SkuID)
                = (newDescription.ColorName, newDescription.ReleaseDate, newDescription.SkuID);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
