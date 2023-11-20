using AutoMapper;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Sales.OutputVMs
{
    public class SaleVmProfiles : Profile
    {
        public SaleVmProfiles()
        {
            CreateMap<Sale, SaleVm>().ForMember(x => x.SaleId, y => y.MapFrom(y => y.SaleId))
                                     .ForMember(x => x.Percent, y => y.MapFrom(y => y.Percent))
                                     .ForMember(x => x.SaleEndDate, y => y.MapFrom(y => y.SaleEndDate));
        }
    }
}
