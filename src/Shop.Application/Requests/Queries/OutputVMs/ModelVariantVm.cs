namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class ModelVariantVm
    {
        public Guid ModelVariantId { get; set; }
        public int ItemsLeft { get; set; }
        public ModelVm Model { get; set; }
        public ModelSizeVm ModelSize { get; set; }
        public PriceVm Price { get; set; }
    }
}
