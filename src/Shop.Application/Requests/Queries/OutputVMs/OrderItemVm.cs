namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class OrderItemVm
    {
        public Guid Id { get; set; }
        public Guid ModelVariantId { get; set; }
        public int Amount { get; set; }
    }
}