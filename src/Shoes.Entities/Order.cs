namespace ShoesShop.Entities
{
    public enum OrderStatus
    {
        Created,
        InProcess,
        Finished,
    }

    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }

        public User Owner { get; }
        public ICollection<OrderItem> Items { get; }

        public Order(Guid orderId, Guid userId, OrderStatus status, DateTime creationDate)
        {
            (UserId, Status, OrderId, CreationDate)
            = (userId, status, orderId, creationDate);
        }

        public Order(Guid userId, OrderStatus status, DateTime? creationDate = null)
            : this(Guid.NewGuid(), userId, status, creationDate ?? DateTime.Now) { }
    }
}
