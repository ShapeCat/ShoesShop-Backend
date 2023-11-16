using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetPriceQuery : IRequest<PriceVm>
    {
        public Guid PriceId { get; set; }
    }

    public class GetPriceQueryHandler : AbstractQueryHandler<GetPriceQuery, PriceVm>
    {
        public GetPriceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<PriceVm> Handle(GetPriceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var priceRepository = UnitOfWork.GetRepositoryOf<Price>();
                var price = await priceRepository.GetAsync(request.PriceId, cancellationToken);
                return Mapper.Map<PriceVm>(price);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
