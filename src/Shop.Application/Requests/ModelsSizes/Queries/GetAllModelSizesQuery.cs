using AutoMapper;
using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ModelsSizes.Queries
{
    public record GetAllModelSizesQuery : IRequest<IEnumerable<ModelSizeVm>> { }

    public class GetAllModelSizesQueryHandler : AbstractQueryHandler<GetAllModelSizesQuery, IEnumerable<ModelSizeVm>>
    {
        public GetAllModelSizesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<IEnumerable<ModelSizeVm>> Handle(GetAllModelSizesQuery request, CancellationToken cancellationToken)
        {
            var modelSizeRepository = UnitOfWork.GetRepositoryOf<ModelSize>();
            var allSizes = await modelSizeRepository.GetAllAsync(cancellationToken);
            return Mapper.Map<IEnumerable<ModelSizeVm>>(allSizes);
        }
    }
}
