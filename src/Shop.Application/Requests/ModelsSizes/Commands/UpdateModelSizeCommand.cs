using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ModelsSizes.Commands
{
    public record UpdateModelSizeCommand : IRequest<Unit>
    {
        public Guid ModelSizeId { get; set; }
        public int Size { get; set; }
    }

    public class UpdateModelSizeCommandValidator : AbstractValidator<UpdateModelSizeCommand>
    {
        public UpdateModelSizeCommandValidator()
        {
            RuleFor(x => x.ModelSizeId).NotEqual(Guid.Empty);
            RuleFor(x => x.Size).GreaterThan(0);
        }
    }

    public class UpdateModelSizeCommandHandler : AbstractCommandHandler<UpdateModelSizeCommand, Unit>
    {
        public UpdateModelSizeCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(UpdateModelSizeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var modelSizeRepository = UnitOfWork.GetRepositoryOf<ModelSize>();
                var newModelSize = new ModelSize()
                {
                    ModelSizeId = request.ModelSizeId,
                    Size = request.Size,
                };
                var sameModelSizes = await modelSizeRepository.FindAllAsync(x => x.Size == newModelSize.Size, cancellationToken);
                if (sameModelSizes.Any()) throw new AlreadyExistsException(newModelSize.Size.ToString(), typeof(ModelSize));
                await modelSizeRepository.EditAsync(newModelSize, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException) { throw; }
            catch (AlreadyExistsException) { throw; }
        }
    }
}
