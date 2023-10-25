using System.Linq.Expressions;
using ShoesShop.Entities;

namespace ShoesShop.Application.Interfaces
{
    public interface IDescriptionRepository
    {
        Task<IEnumerable<Description>> GetAllAsync(CancellationToken cancellationToken);
        Task<Description> GetAsync(Guid descriptionId, CancellationToken cancellationToken);
        Task<IEnumerable<Description>> FindAllAsync(Expression<Func<Description, bool>> predicate, CancellationToken cancellationToken);
        Task AddAsync(Description item, CancellationToken cancellationToken);
        Task EditAsync(Description newItem, CancellationToken cancellationToken);
        Task RemoveAsync(Guid descriptionId, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
