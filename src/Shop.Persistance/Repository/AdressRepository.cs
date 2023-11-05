using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class AdressRepository : GenericRepository<Adress>
    {
        public AdressRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(Adress item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<Adress>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<Adress> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(Adress));
        }

        public override async Task<IEnumerable<Adress>> FindAllAsync(Expression<Func<Adress, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task EditAsync(Adress newItem, CancellationToken cancellationToken)
        {
            var adress = await dbSet.FirstOrDefaultAsync(x => x.Id == newItem.Id, cancellationToken)
                         ?? throw new NotFoundException(newItem.Id.ToString(), typeof(Adress));
            (adress.Country, adress.City, adress.Street, adress.House, adress.Room) =
                (newItem.Country, newItem.City, newItem.Street, newItem.House, newItem.Room);
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var adress = await dbSet.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken)
                         ?? throw new NotFoundException(Id.ToString(), typeof(Adress)); dbSet.Remove(adress);
            dbSet.Remove(adress);
        }
    }
}
