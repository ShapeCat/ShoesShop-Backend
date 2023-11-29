using AutoMapper;
using ShoesShop.Application.Requests.Addresses.Commands;
using ShoesShop.Application.Requests.Authentication.Commands;
using ShoesShop.Application.Requests.Authentication.Queries;
using ShoesShop.Application.Requests.Images.Commands;
using ShoesShop.Application.Requests.Models.Commands;
using ShoesShop.Application.Requests.ModelsSizes.Commands;
using ShoesShop.Application.Requests.ModelsVariants.Commands;
using ShoesShop.Application.Requests.Reviews.Commands;
using ShoesShop.Application.Requests.Sales.Commands;
using ShoesShop.Application.Requests.ShopCartsItems.Commands;
using ShoesShop.Application.Requests.Users.Command;

namespace ShoesShop.WebApi.Dto.Mapping
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

            CreateMap<ImageDto, CreateModelImageCommand>().ForMember(x => x.Url, y => y.MapFrom(x => x.Url))
                                                     .ForMember(x => x.IsPreview, y => y.MapFrom(x => x.IsPreview));

            CreateMap<ImageDto, UpdateImageCommand>().ForMember(x => x.Url, y => y.MapFrom(x => x.Url))
                                                     .ForMember(x => x.IsPreview, y => y.MapFrom(x => x.IsPreview));

            CreateMap<ModelDto, CreateModelCommand>().ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
                                                     .ForMember(x => x.Color, y => y.MapFrom(x => x.Color))
                                                     .ForMember(x => x.Brand, y => y.MapFrom(x => x.Brand))
                                                     .ForMember(x => x.SkuId, y => y.MapFrom(x => x.SkuId))
                                                     .ForMember(x => x.ReleaseDate, y => y.MapFrom(x => x.ReleaseDate));

            CreateMap<ModelDto, UpdateModelCommand>().ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
                                                     .ForMember(x => x.Color, y => y.MapFrom(x => x.Color))
                                                     .ForMember(x => x.Brand, y => y.MapFrom(x => x.Brand))
                                                     .ForMember(x => x.SkuId, y => y.MapFrom(x => x.SkuId))
                                                     .ForMember(x => x.ReleaseDate, y => y.MapFrom(x => x.ReleaseDate));

            CreateMap<ModelSizeDto, CreateModelSizeCommand>().ForMember(x => x.Size, y => y.MapFrom(x => x.Size));

            CreateMap<ModelSizeDto, UpdateModelSizeCommand>().ForMember(x => x.Size, y => y.MapFrom(x => x.Size));

            CreateMap<ModelVariantDto, CreateModelVariantCommand>().ForMember(x => x.ModelId, y => y.MapFrom(x => x.ModelId))
                                                                   .ForMember(x => x.ModelSizeId, y => y.MapFrom(x => x.ModelSizeId))
                                                                   .ForMember(x => x.ItemsLeft, y => y.MapFrom(x => x.ItemsLeft))
                                                                   .ForMember(x => x.Price, y => y.MapFrom(x => x.Price));

            CreateMap<ModelVariantDto, UpdateModelVariantCommand>().ForMember(x => x.ItemsLeft, y => y.MapFrom(x => x.ItemsLeft))
                                                                   .ForMember(x => x.Price, y => y.MapFrom(x => x.Price));

            CreateMap<SaleDto, CreateModelVariantSaleCommand>().ForMember(x => x.Percent, y => y.MapFrom(x => x.Percent))
                                                               .ForMember(x => x.SaleEndDate, y => y.MapFrom(x => x.SaleEndDate));

            CreateMap<SaleDto, UpdateSaleCommand>().ForMember(x => x.Percent, y => y.MapFrom(x => x.Percent))
                                                   .ForMember(x => x.SaleEndDate, y => y.MapFrom(x => x.SaleEndDate));

            CreateMap<LoginDto, CheckUserPasswordQuery>().ForMember(x => x.Login, y => y.MapFrom(x => x.Login))
                                                           .ForMember(x => x.Password, y => y.MapFrom(x => x.Password));

            CreateMap<RegisterDto, RegisterUserCommand>().ForMember(x => x.Login, y => y.MapFrom(x => x.Login))
                                                           .ForMember(x => x.Password, y => y.MapFrom(x => x.Password));

            CreateMap<UserDto, UpdateUserCommand>().ForMember(x => x.UserName, y => y.MapFrom(x => x.UserName))
                                                   .ForMember(x => x.AddressId, y => y.MapFrom(x => x.AddressId))
                                                   .ForMember(x => x.Phone, y => y.MapFrom(x => x.Phone));

            CreateMap<ReviewDto, CreateReviewCommand>().ForMember(x => x.UserId, y => y.MapFrom(x => x.UserId))
                                                       .ForMember(x => x.ModelId, y => y.MapFrom(x => x.ModelId))
                                                       .ForMember(x => x.Rating, y => y.MapFrom(x => x.Rating))
                                                       .ForMember(x => x.Comment, y => y.MapFrom(x => x.Comment))
                                                       .ForMember(x => x.PublishDate, y => y.MapFrom(x => x.PublishDate));

            CreateMap<CartItemDto, AddToShopCartCommand>().ForMember(x => x.ModelVariantId, y => y.MapFrom(x => x.ModeVariantId))
                                                          .ForMember(x => x.Amount, y => y.MapFrom(x => x.Amount));
        }
    }
}
