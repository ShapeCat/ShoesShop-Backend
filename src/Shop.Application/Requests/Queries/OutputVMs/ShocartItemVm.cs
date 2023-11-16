namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class ShocartItemVm
    {
        public Guid ShopcartItemId { get; set; }
        public Guid ModeVariantId { get; set; }
        public int Amount { get; set; }
    }
}