namespace ShoesShop.Application.Requests.Reviews.OutputVMs
{
    public class ReviewVm
    {
        public Guid ReviewId { get; set; }
        public Guid ModelId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? PublishDate { get; set; }
        public byte Rating { get; set; }
        public string Comment { get; set; }
    }
}
