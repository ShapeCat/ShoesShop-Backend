using ShoesShop.Entities;

namespace ShoesShop.Application.Interfaces
{
    public interface IShoesRepository
    {
        Task<IEnumerable<Shoes>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid shoesId, CancellationToken cancellationToken);
        Task<bool> HasDescriptionAsync(Guid shoesId, CancellationToken cancellationToken);
        Task<int> CountSizesAsync(Guid shoesId, CancellationToken cancellationToken);
        Task<Shoes> GetAsync(Guid shoesId, CancellationToken cancellationToken);
        Task<Shoes> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task<Description> GetShoesDescriptionAsync(Guid shoesId, CancellationToken cancellationToken);
        Task<IEnumerable<ShoesSize>> GetShoesSizesAsync(Guid shoesId, CancellationToken cancellationToken);
        Task AddAsync(Shoes item, CancellationToken cancellationToken);
        Task RemoveAsync(Guid shoesId, CancellationToken cancellationToken);
        Task EditNameAsync(Guid shoesId, string newName, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
