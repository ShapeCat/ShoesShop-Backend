namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class ShoesSizeVm
    {
        public Guid SizeId { get; set; }
        public Guid ShoesId { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public int ItemsLeft { get; set; }
    }
}
