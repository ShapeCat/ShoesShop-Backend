using AutoMapper;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetAllModelsQuery : IRequest<IEnumerable<ModelVm>> { }

    public class GetAllModelsQueryHandler : AbstractQueryHandler<GetAllModelsQuery, IEnumerable<ModelVm>>
    {
        public GetAllModelsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<IEnumerable<ModelVm>> Handle(GetAllModelsQuery request, CancellationToken cancellationToken)
        {
            var modelRepository = unitOfWork.GetRepositoryOf<Model>();
            var allModels = await modelRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ModelVm>>(allModels);
        }
    }
}
