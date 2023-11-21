using AutoMapper;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Sales.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Sales.Queries
{
    public record GetSaleQuery : IRequest<SaleVm>
    {
        public Guid SaleId { get; set; }
    }

    public class GetSaleQueryValidator : AbstractValidator<GetSaleQuery>
    {
        public GetSaleQueryValidator()
        {
            RuleFor(x => x.SaleId).NotEmpty();
        }
    }
    public class GetSaleQueryHandler : AbstractQueryHandler<GetSaleQuery, SaleVm>
    {
        public GetSaleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async override Task<SaleVm> Handle(GetSaleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var saleRepository = UnitOfWork.GetRepositoryOf<Sale>();
                var sale = await saleRepository.GetAsync(request.SaleId, cancellationToken);
                return Mapper.Map<SaleVm>(sale);
            }
            catch (NotFoundException) { throw; }
        }
    }
}
