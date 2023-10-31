//using System.Linq.Expressions;
//using Microsoft.EntityFrameworkCore;
//using ShoesShop.Application.Exceptions;
//using ShoesShop.Application.Interfaces;
//using ShoesShop.Entities;

//namespace ShoesShop.Persistence.Repository
//{
//    public class ShoesSizeRepository : IRepositoryOf<ModelSize>
//    {
//        private readonly ShopDbContext dbContext;

//        public ShoesSizeRepository(ShopDbContext dbContext) => this.dbContext = dbContext;

//        public async Task<IEnumerable<ModelSize>> FindAllAsync(Expression<Func<ModelSize, bool>> predicate, CancellationToken cancellationToken)
//        {
//            return await dbContext.Sizes.Where(predicate).ToListAsync(cancellationToken);
//        }

//        public async Task<IEnumerable<ModelSize>> GetAllAsync(CancellationToken cancellationToken)
//        {
//            return await dbContext.Sizes.ToListAsync(cancellationToken);
//        }

//        public async Task<ModelSize> GetAsync(Guid shoesSizeId, CancellationToken cancellationToken)
//        {
//            return await dbContext.Sizes.FirstOrDefaultAsync(x => x.Id == shoesSizeId, cancellationToken)
//                   ?? throw new NotFoundException(shoesSizeId.ToString(), typeof(Model));
//        }

//        public async Task AddAsync(ModelSize item, CancellationToken cancellationToken)
//        {
//            var shoes = await dbContext.Shoes.Where(x => x.Id == item.ShoesId)
//                                 .FirstOrDefaultAsync(x => x.Id == item.ShoesId, cancellationToken)
//                        ?? throw new NotFoundException(item.ShoesId.ToString(), typeof(Model));
//            shoes.Sizes ??= new List<ModelSize>();
//            if (shoes.Sizes.Any(x => x.Size == item.Size))
//            {
//                throw new AlreadyExistsException(item.Size.ToString(), typeof(ModelSize));
//            }
//            shoes.Sizes.Add(item);
//        }

//        public async Task EditAsync(ModelSize newItem, CancellationToken cancellationToken)
//        {
//            var sizeToEdit = await dbContext.Sizes.FirstOrDefaultAsync(x => x.Id == newItem.Id)
//                             ?? throw new NotFoundException(newItem.Id.ToString(), typeof(ModelSize));
//            if (dbContext.Sizes.Any(x => x.Size == newItem.Size && x.ShoesId == sizeToEdit.ShoesId))
//            {
//                throw new AlreadyExistsException(newItem.Size.ToString(), typeof(ModelSize));
//            }
//            (sizeToEdit.Size, sizeToEdit.Price, sizeToEdit.ItemsLeft) = (newItem.Size, newItem.Price, newItem.ItemsLeft);
//        }

//        public async Task RemoveAsync(Guid shoesSizeId, CancellationToken cancellationToken)
//        {
//            var sizeInfo = await dbContext.Sizes.FirstOrDefaultAsync(x => x.Id == shoesSizeId, cancellationToken)
//                        ?? throw new NotFoundException(shoesSizeId.ToString(), typeof(ModelSize));
//            dbContext.Sizes.Remove(sizeInfo);
//        }
//    }
//}
