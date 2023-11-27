using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Users.OutputVMs
{
    public class UserVmProfiles : Profile
    {
        public UserVmProfiles()
        {
            CreateMap<User, UserVm>().ForMember(x => x.UserId, y => y.MapFrom(x => x.UserId))
                                     .ForMember(x => x.UserName, y => y.MapFrom(x => x.UserName))
                                     .ForMember(x => x.AddressId, y => y.MapFrom(x => x.AddressId))
                                     .ForMember(x => x.Phone, y => y.MapFrom(x => x.Phone));
        }
    }
}
