using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ShopCartsItems.OutputVMs
{
    public class ShopCartItemsProfile : Profile
    {
        public ShopCartItemsProfile()
        {
            CreateMap<ShopCartItem, ShopCartItemVm>().ForMember(x => x.ShopCartItemId, y => y.MapFrom(x => x.ShopCartItemId))
                                                     .ForMember(x => x.ModelVariantId, y => y.MapFrom(x => x.ModeVariantId))
                                                     .ForMember(x => x.Amount, y => y.MapFrom(x => x.Amount));
        }
    }
}
