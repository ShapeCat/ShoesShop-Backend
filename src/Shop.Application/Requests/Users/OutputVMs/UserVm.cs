namespace ShoesShop.Application.Requests.Users.OutputVMs
{
    public class UserVm
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid? AddressId { get; set; }
        public string? Phone { get; set; }
    }
}
