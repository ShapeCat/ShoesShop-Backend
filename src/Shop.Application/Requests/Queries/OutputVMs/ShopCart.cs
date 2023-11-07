using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class ShopCart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserVm Owner { get; set; }
        public ICollection<CartItem> Items { get; set; }
    }
}
