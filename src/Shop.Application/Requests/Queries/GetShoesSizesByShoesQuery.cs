using AutoMapper;
using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetShoesSizesByShoesQuery : IRequest<IEnumerable<ShoesSizeVm>>
    {
        public Guid ShoesId { get; set; }
    }

    public class GetShoesSizesByShoesQueryHandler : IRequestHandler<GetShoesSizesByShoesQuery, IEnumerable<ShoesSizeVm>>
    {
        private readonly IShoesRepository shoesRepository;
        private readonly IShoesSizeRepository shoesSizeRepository;
        private readonly IMapper mapper;

        public GetShoesSizesByShoesQueryHandler(IShoesSizeRepository shoesSizeRepository, IShoesRepository shoesRepository,IMapper mapper)
        {
            this.shoesSizeRepository = shoesSizeRepository;
            this.shoesRepository = shoesRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ShoesSizeVm>> Handle(GetShoesSizesByShoesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var parentShoes = await shoesRepository.FindAllAsync(x => x.Id == request.ShoesId, cancellationToken);
                if (parentShoes.Count() == 0) throw new NotFoundException(request.ShoesId.ToString(), typeof(Shoes));
                var shoesSize = await shoesSizeRepository.FindAllAsync(x => x.ShoesId == request.ShoesId, cancellationToken);
                return mapper.Map<IEnumerable<ShoesSizeVm>>(shoesSize);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
