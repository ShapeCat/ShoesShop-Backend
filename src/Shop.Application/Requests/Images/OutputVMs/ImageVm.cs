namespace ShoesShop.Application.Requests.Images.OutputVMs
{
    public class ImageVm
    {
        public Guid ImageId { get; set; }
        public Guid ModelId { get; set; }
        public bool IsPreview { get; set; }
        public string Url { get; set; }
    }
}
