using AutoMapper;
using ShoesShop.Application.Requests.Commands;

namespace ShoesShop.WebApi.Dto.Profiles
{
    public class DtoProfiles : Profile
    {
        public DtoProfiles()
        {
            CreateMap<ShoesDto, CreateShoesCommand>()
                .ForMember(x => x.Name, y => y.MapFrom(x => x.Name));

            CreateMap<ShoesDto, UpdateShoesCommand>()
                .ForMember(x => x.Name, y => y.MapFrom(x => x.Name));

            CreateMap<DescriptionDto, CreateDescriptionCommand>()
                .ForMember(x => x.ReleaseDate, y => y.MapFrom(x => x.ReleaseDate))
                .ForMember(x => x.SkuID, y => y.MapFrom(x => x.SkuID))
                .ForMember(x => x.ColorName, y => y.MapFrom(x => x.ColorName));

            CreateMap<DescriptionDto, UpdateDescriptionCommand>()
                .ForMember(x => x.ReleaseDate, y => y.MapFrom(x => x.ReleaseDate))
                .ForMember(x => x.SkuID, y => y.MapFrom(x => x.SkuID))
                .ForMember(x => x.ColorName, y => y.MapFrom(x => x.ColorName));

            CreateMap<ShoesSizeDto, CreateShoesSizeCommand>()
                .ForMember(x => x.Size, y => y.MapFrom(x => x.Size))
                .ForMember(x => x.Price, y => y.MapFrom(x => x.Price))
                .ForMember(x => x.ItemsLeft, y => y.MapFrom(x => x.ItemsLeft));
        }
    }
}
