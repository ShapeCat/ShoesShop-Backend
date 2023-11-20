using AutoMapper;
using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Sales.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Sales.Queries
{
    public record GetAllSalesQuery : IRequest<IEnumerable<SaleVm>> { }

    public class GetAllSalesQueryHandler : AbstractQueryHandler<GetAllSalesQuery, IEnumerable<SaleVm>>
    {
        public GetAllSalesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async override Task<IEnumerable<SaleVm>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var salesRepository = UnitOfWork.GetRepositoryOf<Sale>();
            var allSales = await salesRepository.GetAllAsync(cancellationToken);
            return Mapper.Map<IEnumerable<SaleVm>>(allSales);
        }
    }
}
