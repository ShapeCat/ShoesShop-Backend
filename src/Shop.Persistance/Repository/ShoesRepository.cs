//using System.Linq.Expressions;
//using Microsoft.EntityFrameworkCore;
//using ShoesShop.Application.Exceptions;
//using ShoesShop.Application.Interfaces;
//using ShoesShop.Entities;

//namespace ShoesShop.Persistence.Repository
//{
//    public class ShoesRepository : IRepositoryOf<Model>
//    {
//        private readonly ShopDbContext dbContext;

//        public ShoesRepository(ShopDbContext dbContext) => this.dbContext = dbContext;

//        public async Task<IEnumerable<Model>> FindAllAsync(Expression<Func<Model, bool>> predicate, CancellationToken cancellationToken)
//        {
//            return await dbContext.Shoes.Where(predicate).ToListAsync(cancellationToken);
//        }

//        public async Task<IEnumerable<Model>> GetAllAsync(CancellationToken cancellationToken)
//        {
//            return await dbContext.Shoes.Include(x => x.Description)
//                                        .Include(x => x.Sizes)
//                                        .ToListAsync(cancellationToken);
//        }

//        public async Task<Model> GetAsync(Guid shoesId, CancellationToken cancellationToken)
//        {
//            return await dbContext.Shoes.Include(x => x.Description)
//                                        .Include(x => x.Sizes)
//                                        .FirstOrDefaultAsync(x => x.Id == shoesId, cancellationToken)
//                   ?? throw new NotFoundException(shoesId.ToString(), typeof(Model));
//        }

//        public async Task AddAsync(Model item, CancellationToken cancellationToken)
//        {
//            await dbContext.Shoes.AddAsync(item, cancellationToken);
//        }

//        public async Task EditAsync(Model newShoes, CancellationToken cancellationToken)
//        {
//            var shoes = await dbContext.Shoes.FirstOrDefaultAsync(x => x.Id == newShoes.Id, cancellationToken)
//                        ?? throw new NotFoundException(newShoes.Id.ToString(), typeof(Model));
//            (shoes.Name)
//                = (newShoes.Name);
//        }

//        public async Task RemoveAsync(Guid shoesId, CancellationToken cancellationToken)
//        {
//            var shoes = await dbContext.Shoes.FirstOrDefaultAsync(x => x.Id == shoesId, cancellationToken)
//                        ?? throw new NotFoundException(shoesId.ToString(), typeof(Model));
//            dbContext.Shoes.Remove(shoes);
//        }
//    }
//}
