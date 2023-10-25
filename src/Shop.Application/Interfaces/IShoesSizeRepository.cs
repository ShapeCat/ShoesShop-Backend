using System.Linq.Expressions;
using ShoesShop.Entities;

namespace ShoesShop.Application.Interfaces
{
    public interface IShoesSizeRepository
    {
        Task<IEnumerable<ShoesSize>> GetAllAsync(CancellationToken cancellationToken);
        Task<ShoesSize> GetAsync(Guid shoesSizeId, CancellationToken cancellationToken);
        Task<IEnumerable<ShoesSize>> FindAllAsync(Expression<Func<ShoesSize, bool>> predicate, CancellationToken cancellationToken);
        Task AddAsync(ShoesSize item, CancellationToken cancellationToken);
        Task EditAsync(ShoesSize newItem, CancellationToken cancellationToken);
        Task RemoveAsync(Guid shoesSizeId, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
