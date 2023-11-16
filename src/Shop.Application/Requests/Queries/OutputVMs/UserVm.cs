namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class UserVm
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public AddressVm Address { get; set; }
    }
}
