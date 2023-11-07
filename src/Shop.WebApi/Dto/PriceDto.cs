namespace ShoesShop.WebApi.Dto
{
    public class PriceDto
    {
        public decimal BasePrice { get; set; }
        public decimal? Sale { get; set; }
        public DateTime? SaleEndDate { get; set; }
    }
}
