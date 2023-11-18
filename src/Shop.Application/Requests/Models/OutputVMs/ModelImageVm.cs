namespace ShoesShop.Application.Requests.Models.OutputVMs
{
    public class ModelImageVm
    {
        public Guid ImageId { get; set; }
        public bool IsPreview { get; set; }
        public string Url { get; set; }
    }
}
