using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries.OutputVMs.Profiles
{
    public class VmProfiles : Profile
    {
        public VmProfiles()
        {
            CreateMap<Shoes, ShoesVm>()
                .ForMember(x => x.ShoesId, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.DescriptionId, y => y.MapFrom(x => x.Description == null ? (Guid?)null : x.Description.Id))
                .ForMember(x => x.SizesIds, y => y.MapFrom(x => x.Sizes == null ? null : x.Sizes.Select(x => x.Id)));

            CreateMap<Description, DescriptionVm>()
                .ForMember(x => x.DescriptionId, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.ShoesId, y => y.MapFrom(x => x.ShoesId))
                .ForMember(x => x.ColorName, y => y.MapFrom(x => x.ColorName))
                .ForMember(x => x.SkuID, y => y.MapFrom(x => x.SkuID))
                .ForMember(x => x.ReleaseDate, y => y.MapFrom(x => x.ReleaseDate));

            CreateMap<ShoesSize, ShoesSizeVm>()
                .ForMember(x => x.SizeId, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.ShoesId, y => y.MapFrom(x => x.ShoesId))
                .ForMember(x => x.Size, y => y.MapFrom(x => x.Size))
                .ForMember(x => x.Price, y => y.MapFrom(x => x.Price))
                .ForMember(x => x.ItemsLeft, y => y.MapFrom(x => x.ItemsLeft));
        }
    }
}