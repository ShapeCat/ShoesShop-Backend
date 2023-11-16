using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Models.OutputVMs
{
    public class ModelVmProfiles : Profile
    {
        public ModelVmProfiles()
        {
            CreateMap<Model, ModelVm>().ForMember(x => x.ModelId, y => y.MapFrom(x => x.ModelId))
                                       .ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
                                       .ForMember(x => x.Color, y => y.MapFrom(x => x.Color))
                                       .ForMember(x => x.Brend, y => y.MapFrom(x => x.Brend))
                                       .ForMember(x => x.SkuId, y => y.MapFrom(x => x.SkuId))
                                       .ForMember(x => x.ReleaseDate, y => y.MapFrom(x => x.ReleaseDate))
                                       .ForMember(x => x.Images, y => y.MapFrom(x => x.Images));
        }
    }
}
