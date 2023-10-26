using AutoMapper;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetAllShoesSizesQuery : IRequest<IEnumerable<ShoesSizeVm>> { }

    public class GetAllShoesSizesQueryHandler : AbstractQuery, IRequestHandler<GetAllShoesSizesQuery, IEnumerable<ShoesSizeVm>>
    {
        public GetAllShoesSizesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<IEnumerable<ShoesSizeVm>> Handle(GetAllShoesSizesQuery request, CancellationToken cancellationToken)
        {
            var shoesSizesRepository = unitOfWork.GetRepositoryOf<ShoesSize>(true);
            var allShoesSizes = await shoesSizesRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ShoesSizeVm>>(allShoesSizes);
        }
    }
}
