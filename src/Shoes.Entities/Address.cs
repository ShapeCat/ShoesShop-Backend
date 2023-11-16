namespace ShoesShop.Entities
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Room { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
