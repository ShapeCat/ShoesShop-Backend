using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class ModelVariantRepository : GenericRepository<ModelVariant>
    {
        public ModelVariantRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<ModelVariant>> FindAllAsync(Expression<Func<ModelVariant, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Include(x => x.Model)
                              .Include(x => x.ModelSize)
                              .Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task<IEnumerable<ModelVariant>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.Include(x => x.Model)
                              .Include(x => x.ModelSize)
                              .ToListAsync(cancellationToken);
        }

        public override async Task<ModelVariant> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.Include(x => x.Model)
                              .Include(x => x.ModelSize)
                              .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(ModelVariant));
        }

        public override async Task EditAsync(ModelVariant newItem, CancellationToken cancellationToken)
        {
            var modelVariant = await dbSet.FirstOrDefaultAsync(x => x.Id == newItem.Id, cancellationToken)
                               ?? throw new NotFoundException(newItem.Id.ToString(), typeof(ModelVariant));
            (modelVariant.ItemsLeft)
                = (newItem.ItemsLeft);
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var modelVariant = await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                               ?? throw new NotFoundException(Id.ToString(), typeof(ModelVariant));
            dbSet.Remove(modelVariant);
        }
    }
}
