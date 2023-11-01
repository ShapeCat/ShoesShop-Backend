namespace ShoesShop.Persistence.Repository
{
    public class OrderItemRepository : GenericRepository<OrderItemRepository>
    {
        public OrderItemRepository(ShopDbContext dbContext) : base(dbContext) { }
    }
}
