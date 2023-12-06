using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Models.OutputVMs
{
    public class ModelVmProfile : Profile
    {
        public ModelVmProfile()
        {
            CreateMap<Model, ModelVm>().ForMember(x => x.ModelId, y => y.MapFrom(x => x.ModelId))
                                       .ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
                                       .ForMember(x => x.Color, y => y.MapFrom(x => x.Color))
                                       .ForMember(x => x.Brand, y => y.MapFrom(x => x.Brand))
                                       .ForMember(x => x.SkuId, y => y.MapFrom(x => x.SkuId))
                                       .ForMember(x => x.ReleaseDate, y => y.MapFrom(x => x.ReleaseDate))
                                       .ForMember(x => x.ImagesIds, y => y.MapFrom(x => from imgs in x.Images select imgs.ImageId));
        }
    }
}
