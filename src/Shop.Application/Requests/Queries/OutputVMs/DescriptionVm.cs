namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class DescriptionVm
    {
        public Guid DescriptionId { get; set; }
        public Guid ShoesId { get; set; }
        public string ColorName { get; set; }
        public string SkuID { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
