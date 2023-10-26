using AutoMapper;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetAllShoesQuery : IRequest<IEnumerable<ShoesVm>> { }

    public class GetAllShoesQueryHandler : AbstractQuery, IRequestHandler<GetAllShoesQuery, IEnumerable<ShoesVm>>
    {
        public GetAllShoesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<IEnumerable<ShoesVm>> Handle(GetAllShoesQuery request, CancellationToken cancellationToken)
        {
            var shoesRepository = unitOfWork.GetRepositoryOf<Shoes>(true);
            var shoes = await shoesRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ShoesVm>>(shoes);
        }
    }
}
