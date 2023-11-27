using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Users.OutputVMs
{
    public class UserVm
    {
    public Roles Role {  get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid? AddressId { get; set; }
        public string? Phone { get; set; }
    }
}
