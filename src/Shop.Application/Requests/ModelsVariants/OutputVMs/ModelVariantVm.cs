using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Application.Requests.Prices.OutputVMs;

namespace ShoesShop.Application.Requests.ModelsVariants.OutputVMs
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
