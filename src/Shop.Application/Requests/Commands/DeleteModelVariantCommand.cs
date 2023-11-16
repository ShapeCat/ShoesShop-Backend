using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record DeleteModelVariantCommand : IRequest<Unit>
    {
        public Guid ModelvariantId { get; set; }
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
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
