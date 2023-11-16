namespace ShoesShop.WebApi.Dto
{
    public class ImageDto
    {
        public Guid ModelId { get; set; }
        public bool IsPreview { get; set; }
        public string Url { get; set; }
    }
}
