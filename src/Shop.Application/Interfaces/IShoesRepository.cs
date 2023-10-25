using System.Linq.Expressions;
using ShoesShop.Entities;

namespace ShoesShop.Application.Interfaces
{
    public interface IShoesRepository
    {
        Task<IEnumerable<Shoes>> GetAllAsync(CancellationToken cancellationToken);
        Task<Shoes> GetAsync(Guid shoesId, CancellationToken cancellationToken);
        Task<IEnumerable<Shoes>> FindAllAsync(Expression<Func<Shoes, bool>> predicate, CancellationToken cancellationToken);
        Task AddAsync(Shoes item, CancellationToken cancellationToken);
        Task EditAsync(Shoes newShoes, CancellationToken cancellationToken);
        Task RemoveAsync(Guid shoesId, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
