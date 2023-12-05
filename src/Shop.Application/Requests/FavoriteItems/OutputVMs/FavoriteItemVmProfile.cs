using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.FavoriteItems.OutputVMs
{
    public class FavoriteItemVmProfile : Profile
    {
        public FavoriteItemVmProfile()
        {
            CreateMap<FavoritesItem, FavoriteItemVm>().ForMember(x => x.FavoriteItemId, y => y.MapFrom(x => x.FavoriteItemId))
                                                      .ForMember(x => x.ModelVariantId, y => y.MapFrom(x => x.ModelVariantId));
        }
    }
}
