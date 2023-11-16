using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class ModelRepository : GenericRepository<Model>
    {
        public ModelRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(Model item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<Model>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<Model> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(Model));
        }

        public override async Task<IEnumerable<Model>> FindAllAsync(Expression<Func<Model, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task EditAsync(Model newItem, CancellationToken cancellationToken)
        {
            var model = await dbSet.FirstOrDefaultAsync(x => x.Id == newItem.Id, cancellationToken)
                        ?? throw new NotFoundException(newItem.Id.ToString(), typeof(Model));
            (model.Name, model.Color, model.Brend, model.SkuId, model.ReleaseDate) =
                (newItem.Name, newItem.Color, newItem.Brend, newItem.SkuId, newItem.ReleaseDate);
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var address = await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                         ?? throw new NotFoundException(Id.ToString(), typeof(Model)); dbSet.Remove(address);
            dbSet.Remove(address);
        }
    }
}
