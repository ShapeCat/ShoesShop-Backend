using AutoMapper;
using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Queries.OutputVMs;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetShoesSizesByShoesQuery : IRequest<IEnumerable<ShoesSizeVm>>
    {
        public Guid ShoesId { get; set; }
    }

    public class GetShoesSizesByShoesQueryHandler : IRequestHandler<GetShoesSizesByShoesQuery, IEnumerable<ShoesSizeVm>>
    {
        private readonly IShoesSizeRepository shoesSizeRepository;
        private readonly IMapper mapper;

        public GetShoesSizesByShoesQueryHandler(IShoesSizeRepository shoesSizeRepository, IMapper mapper)
        {
            this.shoesSizeRepository = shoesSizeRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ShoesSizeVm>> Handle(GetShoesSizesByShoesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var shoesSize = await shoesSizeRepository.GetByShoesAsync(request.ShoesId, cancellationToken);
                return mapper.Map<IEnumerable<ShoesSizeVm>>(shoesSize);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
