namespace ShoesShop.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryOf<T> GetRepositoryOf<T>(bool hasCustomRepository = false) where T : class;
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
