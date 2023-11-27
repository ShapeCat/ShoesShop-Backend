using System.Linq.Expressions;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;

namespace ShoesShop.Application.Common.Extensions
{
    internal static class Extensions
    {
        public static async Task<bool> IsExistsAsync<T>(this IRepositoryOf<T> repository, Guid Id, CancellationToken cancellationToken) where T : class
        {
            try
            {
                await repository.GetAsync(Id, cancellationToken);
            }
            catch (NotFoundException) { return false; }
            return true;
        }

        public static async Task<T> FindFirstAsync<T>(this IRepositoryOf<T> repository, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) where T : class
        {
            var entities = await repository.FindAllAsync(predicate, cancellationToken);
            return entities.First();
        }
    }
}
