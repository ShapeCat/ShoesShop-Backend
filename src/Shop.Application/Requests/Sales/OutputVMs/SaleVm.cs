namespace ShoesShop.Application.Requests.Sales.OutputVMs
{
    public class SaleVm
    {
        public Guid SaleId { get; set; }
        public float Percent { get; set; }
        public DateTime SaleEndDate { get; set; }
    }
}
