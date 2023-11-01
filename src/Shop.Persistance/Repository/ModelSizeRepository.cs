using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class ModelSizeRepository : GenericRepository<ModelSize>
    {
        public ModelSizeRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var modelSize = await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                            ?? throw new NotFoundException(Id.ToString(), typeof(ModelSize));
            dbSet.Remove(modelSize);
        }

        public override async Task EditAsync(ModelSize newItem, CancellationToken cancellationToken)
        {
            var modelSize = await dbSet.FirstOrDefaultAsync(x => x.Id == newItem.Id, cancellationToken)
                            ?? throw new NotFoundException(newItem.Id.ToString(), typeof(ModelSize));
            (modelSize.Size) = (newItem.Size);
        }
    }
}

