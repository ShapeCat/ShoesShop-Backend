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

        public ICollection<User> Users { get; }

        public Address(Guid addressId, string country, string city, string street, string house, int? room)
        {
            (Country, City, Street, House, Room, AddressId)
                = (country, city, street, house, room, addressId);
        }

        public Address(string country, string city, string street, string house, int? room = null)
            : this(Guid.NewGuid(), country, city, street, house, room) { }
    }
}
