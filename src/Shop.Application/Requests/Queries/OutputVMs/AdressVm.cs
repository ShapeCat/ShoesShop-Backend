namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class AdressVm
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Room { get; set; }
    }
}
