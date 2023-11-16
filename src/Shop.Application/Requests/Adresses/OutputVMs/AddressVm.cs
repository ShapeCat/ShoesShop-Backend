namespace ShoesShop.Application.Requests.Adresses.OutputVMs
{
    public class AddressVm
    {
        public Guid AdressId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Room { get; set; }
    }
}