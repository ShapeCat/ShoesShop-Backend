using System.Linq.Expressions;

namespace ShoesShop.Application.Interfaces
{
    public interface IRepositoryOf<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T> GetAsync(Guid Id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task AddAsync(T item, CancellationToken cancellationToken);
        Task EditAsync(T newItem, CancellationToken cancellationToken);
        Task RemoveAsync(Guid Id, CancellationToken cancellationToken);
    }
}
