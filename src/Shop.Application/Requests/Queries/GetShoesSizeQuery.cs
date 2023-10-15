using AutoMapper;
using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Queries.OutputVMs;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetShoesSizeQuery : IRequest<ShoesSizeVm>
    {
        public Guid ShoesSizeId { get; set; }
    }

    public class GetShoesSizeQueryHandler : IRequestHandler<GetShoesSizeQuery, ShoesSizeVm>
    {
        private readonly IShoesSizeRepository shoesSizeRepository;
        private readonly IMapper mapper;

        public GetShoesSizeQueryHandler(IShoesSizeRepository shoesSizeRepository, IMapper mapper)
        {
            this.shoesSizeRepository = shoesSizeRepository;
            this.mapper = mapper;
        }

        public async Task<ShoesSizeVm> Handle(GetShoesSizeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var shoesSize = await shoesSizeRepository.GetAsync(request.ShoesSizeId, cancellationToken);
                return mapper.Map<ShoesSizeVm>(shoesSize);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
