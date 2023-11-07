namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class PriceVm
    {
        public Guid Id { get; set; }
        public decimal BasePrice { get; set; }
        public decimal? Sale { get; set; }
        public DateTime? SaleEndDate { get; set; }
    }
}
