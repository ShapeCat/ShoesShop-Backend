using AutoMapper;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Queries.OutputVMs;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetAllShoesQuery : IRequest<IEnumerable<ShoesVm>> { }

    public class GetAllShoesQueryHandler : IRequestHandler<GetAllShoesQuery, IEnumerable<ShoesVm>>
    {
        private readonly IShoesRepository shoesRepository;
        private readonly IMapper mapper;

        public GetAllShoesQueryHandler(IShoesRepository shoesRepository, IMapper mapper)
        {
            this.shoesRepository = shoesRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ShoesVm>> Handle(GetAllShoesQuery request, CancellationToken cancellationToken)
        {
            var shoes = await shoesRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ShoesVm>>(shoes);
        }
    }
}
