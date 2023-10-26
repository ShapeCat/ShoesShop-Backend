using AutoMapper;
using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetShoesQuery : IRequest<ShoesVm>
    {
        public Guid ShoesId { get; set; }
    }

    public class GetShoesQueryHandler : AbstractQuery, IRequestHandler<GetShoesQuery, ShoesVm>
    {
        public GetShoesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<ShoesVm> Handle(GetShoesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var shoesRepository = unitOfWork.GetRepositoryOf<Shoes>(true);
                var shoes = await shoesRepository.GetAsync(request.ShoesId, cancellationToken);
                return mapper.Map<ShoesVm>(shoes);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
