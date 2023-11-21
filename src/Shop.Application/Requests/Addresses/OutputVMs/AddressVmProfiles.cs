using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Addresses.OutputVMs
{
    public class AddressVmProfiles : Profile
    {
        public AddressVmProfiles()
        {
            CreateMap<Address, AddressVm>().ForMember(x => x.AddressId, y => y.MapFrom(x => x.AddressId))
                                           .ForMember(x => x.Country, y => y.MapFrom(x => x.Country))
                                           .ForMember(x => x.City, y => y.MapFrom(x => x.City))
                                           .ForMember(x => x.Street, y => y.MapFrom(x => x.Street))
                                           .ForMember(x => x.House, y => y.MapFrom(x => x.House))
                                           .ForMember(x => x.Room, y => y.MapFrom(x => x.Room));
        }
    }
}
