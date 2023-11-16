using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Images.OutputVMs
{
    public class ImageVmProfiles : Profile
    {
        public ImageVmProfiles()
        {
            CreateMap<Image, ImageVm>().ForMember(x => x.ImageId, y => y.MapFrom(x => x.ImageId))
                                       .ForMember(x => x.ModelId, y => y.MapFrom(x => x.ModelId))
                                       .ForMember(x => x.IsPreview, y => y.MapFrom(x => x.IsPreview))
                                       .ForMember(x => x.Url, y => y.MapFrom(x => x.Url));
        }
    }
}
