using AutoMapper;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetAllModelSizesQuery : IRequest<IEnumerable<ModelSizeVm>> { }

    public class GetAllModelSizesQueryHandler : AbstractQueryHandler<GetAllModelSizesQuery, IEnumerable<ModelSizeVm>>
    {
        public GetAllModelSizesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async override Task<IEnumerable<ModelSizeVm>> Handle(GetAllModelSizesQuery request, CancellationToken cancellationToken)
        {
            var modelSizeRepository = unitOfWork.GetRepositoryOf<ModelSize>();
            var allSizes = await modelSizeRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ModelSizeVm>>(allSizes);
        }
    }
}
