using AutoMapper;
using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Queries.OutputVMs;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetShoesQuery : IRequest<ShoesVm>
    {
        public Guid ShoesId { get; set; }
    }

    public class GetShoesQueryHandler : IRequestHandler<GetShoesQuery, ShoesVm>
    {
        private readonly IShoesRepository shoesRepository;
        private readonly IMapper mapper;
        public GetShoesQueryHandler(IShoesRepository shoesRepository, IMapper mapper)
        {
            this.shoesRepository = shoesRepository;
            this.mapper = mapper;
        }

        public async Task<ShoesVm> Handle(GetShoesQuery request, CancellationToken cancellationToken)
        {
            try
            {
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
