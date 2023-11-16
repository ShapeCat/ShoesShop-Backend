namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class ReviewVm
    {
        public Guid ReviewId { get; set; }
        public Guid ModelId { get; set; }
        public Guid UserId { get; set; }
        public byte Rating { get; set; }
        public string Comment { get; set; }
        public DateTime PublishDate { get; set; }
    }
}