namespace ShoesShop.WebApi.Dto
{
    public class ReviewDto
    {
        public Guid ModelId { get; set; }
        public Guid UserId { get; set; }
        public byte Rating { get; set; }
        public string Comment { get; set; }
        public DateTime PublishDate { get; set; }
    }
}