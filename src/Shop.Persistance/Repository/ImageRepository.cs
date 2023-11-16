using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class ImageRepository : GenericRepository<Image>
    {
        public ImageRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(Image item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<Image>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<Image> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.ImageId == Id, cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(Image));
        }

        public override async Task<IEnumerable<Image>> FindAllAsync(Expression<Func<Image, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task EditAsync(Image newItem, CancellationToken cancellationToken)
        {
            var image = await dbSet.FirstOrDefaultAsync(x => x.ImageId == newItem.ImageId, cancellationToken)
                        ?? throw new NotFoundException(newItem.ImageId.ToString(), typeof(Image));
            (image.IsPreview, image.Url) =
                (newItem.IsPreview, newItem.Url);
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var image = await dbSet.FirstOrDefaultAsync(x => x.ImageId == Id, cancellationToken)
                        ?? throw new NotFoundException(Id.ToString(), typeof(Image));
            dbSet.Remove(image);
        }
    }
}
