using AutoMapper;
using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetShoesSizeQuery : IRequest<ShoesSizeVm>
    {
        public Guid ShoesSizeId { get; set; }
    }

    public class GetShoesSizeQueryHandler : AbstractQuery, IRequestHandler<GetShoesSizeQuery, ShoesSizeVm>
    {
        public GetShoesSizeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<ShoesSizeVm> Handle(GetShoesSizeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var shoesSizesRepository = unitOfWork.GetRepositoryOf<ShoesSize>(true);
                var shoesSize = await shoesSizesRepository.GetAsync(request.ShoesSizeId, cancellationToken);
                return mapper.Map<ShoesSizeVm>(shoesSize);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
