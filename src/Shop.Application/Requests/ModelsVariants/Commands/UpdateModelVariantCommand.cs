using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ModelsVariants.Commands
{
    public record UpdateModelVariantCommand : IRequest<Unit>
    {
        public Guid ModelVariantId { get; set; }
        public int ItemsLeft { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateModelVariantCommandValidator : AbstractValidator<UpdateModelVariantCommand>
    {
        public UpdateModelVariantCommandValidator()
        {
            RuleFor(x => x.ModelVariantId).NotEqual(Guid.Empty);
            RuleFor(x => x.ItemsLeft).GreaterThanOrEqualTo(0).LessThan(int.MaxValue);
            RuleFor(x => x.Price).GreaterThan(0).LessThan(decimal.MaxValue);
        }
    }

    public class UpdateModelVariantCommandHandler : AbstractCommandHandler<UpdateModelVariantCommand, Unit>
    {
        public UpdateModelVariantCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(UpdateModelVariantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var modelVariantRepository = UnitOfWork.GetRepositoryOf<ModelVariant>();
                var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
                var modelSizeRepository = UnitOfWork.GetRepositoryOf<ModelSize>();
                var modelVariant = await modelVariantRepository.GetAsync(request.ModelVariantId, cancellationToken);
                (modelVariant.ItemsLeft, modelVariant.Price)
                    = (request.ItemsLeft, request.Price);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException) { throw; }
        }
    }
}
