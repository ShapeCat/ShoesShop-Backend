namespace ShoesShop.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid ModelId { get; set; }
        public Guid UserId { get; set; }
        public byte Rating { get; set; }
        public string Comment { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;

        public Model Model { get; set; }
        public User Author { get; set; }
    }
}