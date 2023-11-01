using ShoesShop.Entities;

namespace ShoesShop.Persistence.Repository
{
    public class CartItemRepository : GenericRepository<CartItem>
    {
        public CartItemRepository(ShopDbContext dbContext) : base(dbContext) { }
    }
}
