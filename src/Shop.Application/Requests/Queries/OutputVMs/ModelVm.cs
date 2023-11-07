namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class ModelVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Brend { get; set; }
        public string SkuId { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}