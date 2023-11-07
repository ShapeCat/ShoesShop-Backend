using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class OrderVm
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
