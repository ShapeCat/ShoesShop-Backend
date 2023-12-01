//using System.Linq.Expressions;
//using Microsoft.EntityFrameworkCore;
//using ShoesShop.Application.Common.Exceptions;
//using ShoesShop.Entities;

//namespace ShoesShop.Persistence.Repository
//{
//    public class FavoritesListRepository : GenericRepository<FavoritesList>
//    {
//        public FavoritesListRepository(ShopDbContext dbContext) : base(dbContext) { }

//        public override async Task AddAsync(FavoritesList item, CancellationToken cancellationToken)
//        {
//            await dbSet.AddAsync(item, cancellationToken);
//        }

//        public override async Task<IEnumerable<FavoritesList>> GetAllAsync(CancellationToken cancellationToken)
//        {
//            return await dbSet.ToListAsync(cancellationToken);
//        }

//        public override async Task<FavoritesList> GetAsync(Guid Id, CancellationToken cancellationToken)
//        {
//            return await dbSet.FirstOrDefaultAsync(x => x.FavoriteListId == Id, cancellationToken)
//                ?? throw new NotFoundException(Id.ToString(), typeof(FavoritesList));
//        }

//        public override async Task<IEnumerable<FavoritesList>> FindAllAsync(Expression<Func<FavoritesList, bool>> predicate, CancellationToken cancellationToken)
//        {
//            return await dbSet.Where(predicate)
//                              .ToListAsync(cancellationToken);
//        }
//    }
//}
