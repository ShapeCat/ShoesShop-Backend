using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ModelsVariants.OutputVMs
{
    public class ModelVariantVmProfiles : Profile
    {
        public ModelVariantVmProfiles()
        {
            CreateMap<ModelVariant, ModelVariantVm>().ForMember(x => x.ModelVariantId, y => y.MapFrom(x => x.ModelVariantId))
                                                     .ForMember(x => x.ItemsLeft, y => y.MapFrom(x => x.ItemsLeft))
                                                     .ForMember(x => x.Model, y => y.MapFrom(x => x.Model))
                                                     .ForMember(x => x.ModelSize, y => y.MapFrom(x => x.ModelSize))
                                                     .ForMember(x => x.Price, y => y.MapFrom(x => x.Price));
        }
    }
}
