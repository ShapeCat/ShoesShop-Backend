//using AutoMapper;
//using MediatR;
//using ShoesShop.Application.Common.Interfaces;
//using ShoesShop.Application.Requests.Abstraction;
//using ShoesShop.Application.Requests.Prices.OutputVMs;
//using ShoesShop.Entities;

//namespace ShoesShop.Application.Requests.Prices.Queries
//{
//    public record GetAllPricesQuery : IRequest<IEnumerable<PriceVm>> { }

//    public class GetAllPricesQueryHandler : AbstractQueryHandler<GetAllPricesQuery, IEnumerable<PriceVm>>
//    {
//        public GetAllPricesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

//        public override async Task<IEnumerable<PriceVm>> Handle(GetAllPricesQuery request, CancellationToken cancellationToken)
//        {
//            var pricePerository = UnitOfWork.GetRepositoryOf<Price>();
//            var allPrices = await pricePerository.GetAllAsync(cancellationToken);
//            return Mapper.Map<IEnumerable<PriceVm>>(allPrices);
//        }
//    }
//}
