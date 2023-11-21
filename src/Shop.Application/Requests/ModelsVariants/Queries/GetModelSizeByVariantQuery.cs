using AutoMapper;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ModelsVariants.Queries
{
    public record GetModelSizeByVariantQuery : IRequest<ModelSizeVm>
    {
        public Guid ModelVariantId { get; set; }
    }

    public class GetModelSizeByVariantQueryValidator : AbstractValidator<GetModelSizeByVariantQuery>
    {
        public GetModelSizeByVariantQueryValidator()
        {
            RuleFor(x => x.ModelVariantId).NotEqual(Guid.Empty);
        }
    }

    public class GetModelSizeByVariantQueryHandler : AbstractQueryHandler<GetModelSizeByVariantQuery, ModelSizeVm>
    {
        public GetModelSizeByVariantQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<ModelSizeVm> Handle(GetModelSizeByVariantQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var modelSizeRepository = UnitOfWork.GetRepositoryOf<ModelSize>();
                var modelVariantRepository = UnitOfWork.GetRepositoryOf<ModelVariant>();
                var modelVariant = await modelVariantRepository.GetAsync(request.ModelVariantId, cancellationToken);
                var modelSize = await modelSizeRepository.GetAsync(modelVariant.ModelSizeId, cancellationToken);
                return Mapper.Map<ModelSizeVm>(modelSize);
            }
            catch (NotFoundException) { throw; }
        }
    }
}
