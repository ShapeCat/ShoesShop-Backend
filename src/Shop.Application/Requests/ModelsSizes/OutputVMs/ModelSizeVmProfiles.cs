using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ModelsSizes.OutputVMs
{
    public class ModelSizeVmProfiles : Profile
    {
        public ModelSizeVmProfiles()
        {
            CreateMap<ModelSize, ModelSizeVm>().ForMember(x => x.ModelSizeId, y => y.MapFrom(x => x.ModelSizeId))
                                               .ForMember(x => x.Size, y => y.MapFrom(x => x.Size));

        }
    }
}
