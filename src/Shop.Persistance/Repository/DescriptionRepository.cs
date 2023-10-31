//using System.Linq.Expressions;
//using Microsoft.EntityFrameworkCore;
//using ShoesShop.Application.Exceptions;
//using ShoesShop.Application.Interfaces;
//using ShoesShop.Entities;

//namespace ShoesShop.Persistence.Repository
//{
//    public class DescriptionRepository : IRepositoryOf<Description>
//    {
//        private readonly ShopDbContext dbContext;

//        public DescriptionRepository(ShopDbContext dbContext) => this.dbContext = dbContext;

//        public async Task<IEnumerable<Description>> FindAllAsync(Expression<Func<Description, bool>> predicate, CancellationToken cancellationToken)
//        {
//            return await dbContext.Descriptions.Where(predicate).ToListAsync(cancellationToken);
//        }

//        public async Task<IEnumerable<Description>> GetAllAsync(CancellationToken cancellationToken)
//        {
//            return await dbContext.Descriptions.ToListAsync(cancellationToken);
//        }

//        public async Task<Description> GetAsync(Guid descriptionId, CancellationToken cancellationToken)
//        {
//            return await dbContext.Descriptions.FirstOrDefaultAsync(x => x.Id == descriptionId, cancellationToken)
//                   ?? throw new NotFoundException(descriptionId.ToString(), typeof(Description));
//        }

//        public async Task AddAsync(Description item, CancellationToken cancellationToken)
//        {
//            var shoes = await dbContext.Shoes.Where(x => x.Id == item.ShoesId)
//                                             .Include(x => x.Description)
//                                             .SingleOrDefaultAsync(cancellationToken)
//                        ?? throw new NotFoundException(item.ShoesId.ToString(), typeof(Model));
//            if (shoes.Description is not null) throw new AlreadyExistsException(item.ShoesId.ToString(), typeof(Description));
//            shoes.Description = item;
//        }

//        public async Task EditAsync(Description newItem, CancellationToken cancellationToken)
//        {
//            var description = await dbContext.Descriptions.FirstOrDefaultAsync(x => x.Id == newItem.Id, cancellationToken)
//                              ?? throw new NotFoundException(newItem.Id.ToString(), typeof(Description));
//            (description.ColorName, description.ReleaseDate, description.SkuID)
//                = (newItem.ColorName, newItem.ReleaseDate, newItem.SkuID);
//        }

//        public async Task RemoveAsync(Guid descriptionId, CancellationToken cancellationToken)
//        {
//            var description = await dbContext.Descriptions.FirstOrDefaultAsync(x => x.Id == descriptionId, cancellationToken)
//                              ?? throw new NotFoundException(descriptionId.ToString(), typeof(Description));
//            dbContext.Descriptions.Remove(description);
//        }
//    }
//}
