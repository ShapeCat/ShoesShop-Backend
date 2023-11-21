namespace ShoesShop.Application.Requests.Models.OutputVMs
{
    public class ModelVm
    {
        public Guid ModelId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string SkuId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<ModelImageVm> Images { get; set; }
    }
}