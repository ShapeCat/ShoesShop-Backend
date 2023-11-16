using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Prices.OutputVMs
{
    public class PriceVmProfiles : Profile
    {
        public PriceVmProfiles()
        {
            CreateMap<Price, PriceVm>().ForMember(x => x.PriceId, y => y.MapFrom(x => x.PriceId))
                                       .ForMember(x => x.BasePrice, y => y.MapFrom(x => x.BasePrice))
                                       .ForMember(x => x.Sale, y => y.MapFrom(x => x.Sale))
                                       .ForMember(x => x.SaleEndDate, y => y.MapFrom(x => x.SaleEndDate));

        }
    }
}
