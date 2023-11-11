﻿using AutoMapper;
using ShoesShop.Application.Requests.Commands;

namespace ShoesShop.WebApi.Dto.Profiles
{
    public class DtoProfiles : Profile
    {
        public DtoProfiles()
        {
            CreateMap<AdressDto, CreateAdressCommand>().ForMember(x => x.Country, y => y.MapFrom(x => x.Country))
                                                       .ForMember(x => x.City, y => y.MapFrom(x => x.City))
                                                       .ForMember(x => x.Street, y => y.MapFrom(x => x.Street))
                                                       .ForMember(x => x.House, y => y.MapFrom(x => x.House))
                                                       .ForMember(x => x.Room, y => y.MapFrom(x => x.Room));

            CreateMap<AdressDto, UpdateAdressCommand>().ForMember(x => x.Country, y => y.MapFrom(x => x.Country))
                                                       .ForMember(x => x.City, y => y.MapFrom(x => x.City))
                                                       .ForMember(x => x.Street, y => y.MapFrom(x => x.Street))
                                                       .ForMember(x => x.House, y => y.MapFrom(x => x.House))
                                                       .ForMember(x => x.Room, y => y.MapFrom(x => x.Room));
        }
    }
}
