namespace ShoesShop.Entities
{
    public enum OrderStatus
    {
        Created,
        InProcess,
        Finished
    }

    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }

        public User Owner { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
