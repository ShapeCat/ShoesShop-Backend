using ShoesShop.Entities;

namespace ShoesShop.Application.Interfaces
{
    public interface IShoesSizeRepository
    {
        Task<IEnumerable<ShoesSize>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid shoesSizeId, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid shoesId, int size, CancellationToken cancellationToken);
        Task<ShoesSize> GetAsync(Guid shoesSizeId, CancellationToken cancellationToken);
        Task<ShoesSize> GetByShoesAsync(Guid shoesId, int size, CancellationToken cancellationToken);
        Task<IEnumerable<ShoesSize>> GetByShoesAsync(Guid shoesId, CancellationToken cancellationToken);
        Task AddAsync(Guid shoesId, ShoesSize item, CancellationToken cancellationToken);
        Task RemoveAsync(Guid shoesSizeId, CancellationToken cancellationToken);
        Task EditAsync(Guid shoesSizeId, ShoesSize newItem, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
