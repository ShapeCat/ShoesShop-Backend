using AutoMapper;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ModelsVariants.Commands
{
    public record GetModelByVariantQuery : IRequest<ModelVm>
    {
        public Guid ModelVariantId { get; set; }
    }

    public class GetModelByVariantQueryHandler : AbstractQueryHandler<GetModelByVariantQuery, ModelVm>
    {
        public GetModelByVariantQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<ModelVm> Handle(GetModelByVariantQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
                var modelVariantRepository = UnitOfWork.GetRepositoryOf<ModelVariant>();
                var modelVariant = await modelVariantRepository.GetAsync(request.ModelVariantId, cancellationToken);
                var model = await modelRepository.GetAsync(modelVariant.ModelId, cancellationToken);
                return Mapper.Map<ModelVm>(model);
            }
            catch (NotFoundException) { throw; }
        }
    }
}
