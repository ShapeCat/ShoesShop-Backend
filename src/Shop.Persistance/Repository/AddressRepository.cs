using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class AddressRepository : GenericRepository<Address>
    {
        public AddressRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(Address item, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<Address>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<Address> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.AddressId == Id, cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(Address));
        }

        public override async Task<IEnumerable<Address>> FindAllAsync(Expression<Func<Address, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task EditAsync(Address newItem, CancellationToken cancellationToken)
        {
            var address = await dbSet.FirstOrDefaultAsync(x => x.AddressId == newItem.AddressId, cancellationToken)
                         ?? throw new NotFoundException(newItem.AddressId.ToString(), typeof(Address));
            (address.Country, address.City, address.Street, address.House, address.Room) =
                (newItem.Country, newItem.City, newItem.Street, newItem.House, newItem.Room);
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var address = await dbSet.FirstOrDefaultAsync(x => x.AddressId == Id, cancellationToken)
                         ?? throw new NotFoundException(Id.ToString(), typeof(Address)); dbSet.Remove(address);
            dbSet.Remove(address);
        }
    }
}
