using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Authentication.OutputVMs
{
    public class AuthenticationUserProfiles : Profile
    {
        public AuthenticationUserProfiles()
        {
            CreateMap<User, AuthenticatedUserData>().ForMember(x => x.UserName, y => y.MapFrom(x => x.UserName))
                                                    .ForMember(x => x.Role, y => y.MapFrom(x => x.Role))
                                                    .ForMember(x => x.UserId, y => y.MapFrom(x => x.UserId));
        }
    }
}
