using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ModelsVariants.Commands
{
    public record DeleteModelVariantCommand : IRequest<Unit>
    {
        public Guid ModelvariantId { get; set; }
    }

    public class DeleteModelVariantCommandValidator : AbstractValidator<DeleteModelVariantCommand> {
        public DeleteModelVariantCommandValidator()
        {
            RuleFor(x => x.ModelvariantId).NotEqual(Guid.Empty);
        }
    }
    public class DeleteModelVariantCommandHandler : AbstractCommandHandler<DeleteModelVariantCommand, Unit>
    {
        public DeleteModelVariantCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(DeleteModelVariantCommand request, CancellationToken cancellationToken)
        {
            try

            {
                var modelVariantRepository = UnitOfWork.GetRepositoryOf<ModelVariant>();
                await modelVariantRepository.RemoveAsync(request.ModelvariantId, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException) { throw; }
        }
    }
}
