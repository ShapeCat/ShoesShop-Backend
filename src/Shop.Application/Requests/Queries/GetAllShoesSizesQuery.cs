using AutoMapper;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Queries.OutputVMs;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetAllShoesSizesQuery : IRequest<IEnumerable<ShoesSizeVm>> { }

    public class GetAllShoesSizesQueryHandler : IRequestHandler<GetAllShoesSizesQuery, IEnumerable<ShoesSizeVm>>
    {
        private readonly IShoesSizeRepository shoesSizeRepository;
        private readonly IMapper mapper;

        public GetAllShoesSizesQueryHandler(IShoesSizeRepository shoesSizeRepository, IMapper mapper)
        {
            this.shoesSizeRepository = shoesSizeRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ShoesSizeVm>> Handle(GetAllShoesSizesQuery request, CancellationToken cancellationToken)
        {
            var allShoesSizes = await shoesSizeRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ShoesSizeVm>>(allShoesSizes);
        }
    }
}
