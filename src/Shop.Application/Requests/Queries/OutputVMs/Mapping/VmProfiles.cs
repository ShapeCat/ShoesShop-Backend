using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries.OutputVMs.Profiles
{
    public class VmProfiles : Profile
    {
        public VmProfiles()
        {
            CreateMap<Address, AddressVm>().ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                             .ForMember(x => x.Country, y => y.MapFrom(x => x.Country))
                             .ForMember(x => x.City, y => y.MapFrom(x => x.City))
                             .ForMember(x => x.Street, y => y.MapFrom(x => x.Street))
                             .ForMember(x => x.House, y => y.MapFrom(x => x.House))
                             .ForMember(x => x.Room, y => y.MapFrom(x => x.Room));

            CreateMap<ShopcartItem, CartItemVm>().ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                                             .ForMember(x => x.ModeVariantId, y => y.MapFrom(x => x.ModeVariantId))
                                             .ForMember(x => x.Amount, y => y.MapFrom(x => x.Amount));

            CreateMap<FavoritesItem, FavoritesItemVm>().ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                                                       .ForMember(x => x.ModelVariantId, y => y.MapFrom(x => x.ModelVariantId));

            CreateMap<Image, ImageVm>().ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                                       .ForMember(x => x.ModelId, y => y.MapFrom(x => x.ModelId))
                                       .ForMember(x => x.IsPreview, y => y.MapFrom(x => x.IsPreview))
                                       .ForMember(x => x.Url, y => y.MapFrom(x => x.Url));

            CreateMap<ModelSize, ModelSizeVm>().ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                                               .ForMember(x => x.Size, y => y.MapFrom(x => x.Size));

            CreateMap<ModelVariant, ModelVariantVm>().ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                                                     .ForMember(x => x.ItemsLeft, y => y.MapFrom(x => x.ItemsLeft))
                                                     .ForMember(x => x.Model, y => y.MapFrom(x => x.Model))
                                                     .ForMember(x => x.ModelSize, y => y.MapFrom(x => x.ModelSize))
                                                     .ForMember(x => x.Price, y => y.MapFrom(x => x.Price));

            CreateMap<Model, ModelVm>().ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                                       .ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
                                       .ForMember(x => x.Color, y => y.MapFrom(x => x.Color))
                                       .ForMember(x => x.Brend, y => y.MapFrom(x => x.Brend))
                                       .ForMember(x => x.SkuId, y => y.MapFrom(x => x.SkuId))
                                       .ForMember(x => x.ReleaseDate, y => y.MapFrom(x => x.ReleaseDate))
                                       .ForMember(x=> x.Images, y => y.MapFrom(x => x.Images));

            CreateMap<OrderItem, OrderItemVm>().ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                                               .ForMember(x => x.ModelVariantId, y => y.MapFrom(x => x.ModelVariantId))
                                               .ForMember(x => x.Amount, y => y.MapFrom(x => x.Amount));


            CreateMap<Price, PriceVm>().ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                                       .ForMember(x => x.BasePrice, y => y.MapFrom(x => x.BasePrice))
                                       .ForMember(x => x.Sale, y => y.MapFrom(x => x.Sale))
                                       .ForMember(x => x.SaleEndDate, y => y.MapFrom(x => x.SaleEndDate));

            CreateMap<Review, ReviewVm>().ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                                         .ForMember(x => x.ModelId, y => y.MapFrom(x => x.ModelId))
                                         .ForMember(x => x.UserId, y => y.MapFrom(x => x.UserId))
                                         .ForMember(x => x.Rating, y => y.MapFrom(x => x.Rating))
                                         .ForMember(x => x.Comment, y => y.MapFrom(x => x.Comment))
                                         .ForMember(x => x.PublishDate, y => y.MapFrom(x => x.PublishDate));

            CreateMap<User, UserVm>().ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                                     .ForMember(x => x.UserName, y => y.MapFrom(x => x.UserName))
                                     .ForMember(x => x.Login, y => y.MapFrom(x => x.Login))
                                     .ForMember(x => x.Phone, y => y.MapFrom(x => x.Phone))
                                     .ForMember(x => x.Address, y => y.MapFrom(x => x.Address));
        }
    }
}