namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class ShoesVm
    {
        public Guid ShoesId { get; set; }
        public string Name { get; set; }
        public Guid? DescriptionId { get; set; }
        public ICollection<Guid>? SizesIds { get; set; }
    }
}
