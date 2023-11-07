namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class CartItemVm
    {
        public Guid Id { get; set; }
        public Guid ModeVariantId { get; set; }
        public int Amount { get; set; }
    }
}
