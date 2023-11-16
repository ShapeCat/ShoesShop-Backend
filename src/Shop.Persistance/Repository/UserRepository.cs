using System.Linq.Expressions;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(ShopDbContext dbContext) : base(dbContext) { }

        public override async Task AddAsync(User item, CancellationToken cancellationToken)
        {
            item.Password = SHA256.HashData(item.Password);
            await dbSet.AddAsync(item, cancellationToken);
        }

        public override async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<User> GetAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.UserId == Id, cancellationToken)
                   ?? throw new NotFoundException(Id.ToString(), typeof(User));
        }

        public override async Task<IEnumerable<User>> FindAllAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet.Where(predicate)
                              .ToListAsync(cancellationToken);
        }

        public override async Task EditAsync(User newItem, CancellationToken cancellationToken)
        {
            var user = await dbSet.FirstOrDefaultAsync(x => x.UserId == newItem.UserId, cancellationToken)
                        ?? throw new NotFoundException(newItem.UserId.ToString(), typeof(User));
            (user.UserName, user.Phone, user.Login)
                = (newItem.UserName, newItem.Phone, newItem.Login);
        }

        public override async Task RemoveAsync(Guid Id, CancellationToken cancellationToken)
        {
            var user = await dbSet.FirstOrDefaultAsync(x => x.UserId == Id, cancellationToken)
                        ?? throw new NotFoundException(Id.ToString(), typeof(User));
            dbSet.Remove(user);
        }
    }
}
