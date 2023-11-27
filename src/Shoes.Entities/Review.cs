namespace ShoesShop.Entities
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid ModelId { get; set; }
        public Guid UserId { get; set; }
        public DateTime PublishDate { get; set; }
        public byte Rating { get; set; }
        public string Comment { get; set; }

        public Model Model { get; }
        public User Author { get; }

        public Review(Guid reviewId, Guid modelId, Guid userId, DateTime publishDate, byte rating, string comment)
        {
            (ReviewId, ModelId, UserId, Rating, Comment)
                = (reviewId, modelId, userId, rating, comment);
        }

        public Review(Guid modelId, Guid userId, byte rating, string comment, DateTime? publishDate = null)
            : this(Guid.NewGuid(), modelId, userId, publishDate ?? DateTime.Now, rating, comment) { }
    }
}