using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class ModelSizeRepository : GenericRepository<ModelSize>
    {
        public ModelSizeRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(ModelSize item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<ModelSize>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<ModelSize> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.ModelSizeId == Id, cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(ModelSize));
        }

        public override async Task<IEnumerable<ModelSize>> FindAllAsync(Expression<Func<ModelSize, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task EditAsync(ModelSize newItem, CancellationToken cancellationToken)
        {
            var modelSize = await dbSet.FirstOrDefaultAsync(x => x.ModelSizeId == newItem.ModelSizeId, cancellationToken)
                            ?? throw new NotFoundException(newItem.ModelSizeId.ToString(), typeof(ModelSize));
            modelSize.Size = newItem.Size;
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var modelSize = await dbSet.FirstOrDefaultAsync(x => x.ModelSizeId == Id, cancellationToken)
                            ?? throw new NotFoundException(Id.ToString(), typeof(ModelSize));
            dbSet.Remove(modelSize);
        }
    }
}

