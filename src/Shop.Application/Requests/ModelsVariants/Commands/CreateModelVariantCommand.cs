using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ModelsVariants.Commands
{
    public record CreateModelVariantCommand : IRequest<Guid>
    {
        public Guid ModelId { get; set; }
        public Guid ModelSizeId { get; set; }
        public int ItemsLeft { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateModelVariantCommandValidator : AbstractValidator<CreateModelVariantCommand>
    {
        public CreateModelVariantCommandValidator()
        {
            RuleFor(x => x.ModelId).NotEqual(Guid.Empty);
            RuleFor(x => x.ModelSizeId).NotEqual(Guid.Empty);
            RuleFor(x => x.ItemsLeft).GreaterThanOrEqualTo(0).LessThan(int.MaxValue);
            RuleFor(x => x.Price).GreaterThan(0).LessThan(decimal.MaxValue);
        }
    }

    public class CreateModelVariantCommandHandler : AbstractCommandHandler<CreateModelVariantCommand, Guid>
    {
        public CreateModelVariantCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(CreateModelVariantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var modelVariantRepository = UnitOfWork.GetRepositoryOf<ModelVariant>();
                var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
                var modelSizeRepository = UnitOfWork.GetRepositoryOf<ModelSize>();
                var model = await modelRepository.GetAsync(request.ModelId, cancellationToken);
                var modelSize = await modelSizeRepository.GetAsync(request.ModelSizeId, cancellationToken);
                var modelVariant = new ModelVariant(model.ModelId, modelSize.ModelSizeId, request.ItemsLeft, request.Price);
                await modelVariantRepository.AddAsync(modelVariant, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return modelVariant.ModelVariantId;
            }
            catch (NotFoundException) { throw; }
        }
    }
}
