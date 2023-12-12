namespace ShoesShop.WebApi.Dto
{
    public class ReviewDto
    {
        public byte Rating { get; set; }
        public string Comment { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}