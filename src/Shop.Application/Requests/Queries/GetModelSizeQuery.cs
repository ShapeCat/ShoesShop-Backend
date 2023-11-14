using AutoMapper;
using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Queries
{
    public record GetModelSizeQuery : IRequest<ModelSizeVm>
    {
        public Guid ModelSizeId { get; set; }
    }

    public class GetModelSizeQueryHandler : AbstractQueryHandler<GetModelSizeQuery, ModelSizeVm>
    {
        public GetModelSizeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<ModelSizeVm> Handle(GetModelSizeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var modelSizeRepository = unitOfWork.GetRepositoryOf<ModelSize>();
                var modelSize = await modelSizeRepository.GetAsync(request.ModelSizeId, cancellationToken);
                return mapper.Map<ModelSizeVm>(modelSize);
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
