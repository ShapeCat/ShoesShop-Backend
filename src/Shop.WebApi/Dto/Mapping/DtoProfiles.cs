using AutoMapper;
using ShoesShop.Application.Requests.Adresses.Commands;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Application.Requests.Images.Commands;
using ShoesShop.Application.Requests.Models.Commands;
using ShoesShop.Application.Requests.ModelsSizes.Commands;
using ShoesShop.Application.Requests.ModelsVariants.Commands;
using ShoesShop.Application.Requests.Prices.Commands;

namespace ShoesShop.WebApi.Dto.Profiles
{
    public class DtoProfiles : Profile
    {
        public DtoProfiles()
        {
            CreateMap<AddressDto, CreateAddressCommand>().ForMember(x => x.Country, y => y.MapFrom(x => x.Country))
                                                       .ForMember(x => x.City, y => y.MapFrom(x => x.City))
                                                       .ForMember(x => x.Street, y => y.MapFrom(x => x.Street))
                                                       .ForMember(x => x.House, y => y.MapFrom(x => x.House))
                                                       .ForMember(x => x.Room, y => y.MapFrom(x => x.Room));

            CreateMap<AddressDto, UpdateAddressCommand>().ForMember(x => x.Country, y => y.MapFrom(x => x.Country))
                                                       .ForMember(x => x.City, y => y.MapFrom(x => x.City))
                                                       .ForMember(x => x.Street, y => y.MapFrom(x => x.Street))
                                                       .ForMember(x => x.House, y => y.MapFrom(x => x.House))
                                                       .ForMember(x => x.Room, y => y.MapFrom(x => x.Room));

            CreateMap<ImageDto, CreateImageCommand>().ForMember(x => x.Url, y => y.MapFrom(x => x.Url))
                                                     .ForMember(x => x.IsPreview, y => y.MapFrom(x => x.IsPreview));

            CreateMap<ImageDto, UpdateImageCommand>().ForMember(x => x.Url, y => y.MapFrom(x => x.Url))
                                                     .ForMember(x => x.IsPreview, y => y.MapFrom(x => x.IsPreview));

            CreateMap<ModelDto, CreateModelCommand>().ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
                                                     .ForMember(x => x.Color, y => y.MapFrom(x => x.Color))
                                                     .ForMember(x => x.Brend, y => y.MapFrom(x => x.Brend))
                                                     .ForMember(x => x.SkuId, y => y.MapFrom(x => x.SkuId))
                                                     .ForMember(x => x.ReleaseDate, y => y.MapFrom(x => x.ReleaseDate));

            CreateMap<ModelDto, UpdateModelCommand>().ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
                                                     .ForMember(x => x.Color, y => y.MapFrom(x => x.Color))
                                                     .ForMember(x => x.Brend, y => y.MapFrom(x => x.Brend))
                                                     .ForMember(x => x.SkuId, y => y.MapFrom(x => x.SkuId))
                                                     .ForMember(x => x.ReleaseDate, y => y.MapFrom(x => x.ReleaseDate));

            CreateMap<ModelSizeDto, CreateModelSizeCommand>().ForMember(x => x.Size, y => y.MapFrom(x => x.Size));

            CreateMap<ModelSizeDto, UpdateModelSizeCommand>().ForMember(x => x.Size, y => y.MapFrom(x => x.Size));

            CreateMap<ModelVariantDto, CreateModelVariantCommand>().ForMember(x => x.ModelId, y => y.MapFrom(x => x.ModelId))
                                                                   .ForMember(x => x.ModelSizeId, y => y.MapFrom(x => x.ModelSizeId))
                                                                   .ForMember(x => x.ItemsLeft, y => y.MapFrom(x => x.ItemsLeft));

            CreateMap<ModelVariantDto, UpdateModelVariantCommand>().ForMember(x => x.ModelId, y => y.MapFrom(x => x.ModelId))
                                                                   .ForMember(x => x.ModelSizeId, y => y.MapFrom(x => x.ModelSizeId))
                                                                   .ForMember(x => x.ItemsLeft, y => y.MapFrom(x => x.ItemsLeft));

            CreateMap<PriceDto, UpdatePriceCommand>().ForMember(x => x.BasePrice, y => y.MapFrom(x => x.BasePrice));
        }
    }
}
