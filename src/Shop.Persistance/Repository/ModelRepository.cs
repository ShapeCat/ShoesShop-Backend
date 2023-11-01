using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    internal class ModelRepository : GenericRepository<Model>
    {
        public ModelRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task EditAsync(Model newItem, CancellationToken cancellationToken)
        {
            var model = await dbSet.FirstOrDefaultAsync(x => x.Id == newItem.Id, cancellationToken)
                        ?? throw new NotFoundException(newItem.Id.ToString(), typeof(Model));
            (model.Name, model.Color, model.Brend, model.SkuId, model.ReleaseDate) =
                (newItem.Name, newItem.Color, newItem.Brend, newItem.SkuId, newItem.ReleaseDate);
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var adress = await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                         ?? throw new NotFoundException(Id.ToString(), typeof(Model)); dbSet.Remove(adress);
            dbSet.Remove(adress);
        }
    }
}
