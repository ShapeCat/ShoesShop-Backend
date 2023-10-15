using ShoesShop.Entities;

namespace ShoesShop.Application.Interfaces
{
    public interface IDescriptionRepository
    {
        Task<IEnumerable<Description>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid descriptionId, CancellationToken cancellationToken);
        Task<Description> GetAsync(Guid descriptionId, CancellationToken cancellationToken);
        Task<Description> GetByShoesAsync(Guid shoesId, CancellationToken cancellationToken);
        Task AddAsync(Guid shoesId, Description item, CancellationToken cancellationToken);
        Task RemoveAsync(Guid descriptionId, CancellationToken cancellationToken);
        Task EditAsync(Guid descriptionId, Description newItem, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
