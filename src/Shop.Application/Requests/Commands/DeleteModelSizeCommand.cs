using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record DeleteModelSizeCommand : IRequest<Unit>
    {
        public Guid ModelSizeId { get; set; }
    }

    public class DeleteModelSizeCommandHandler : AbstractCommandHandler<DeleteModelSizeCommand, Unit>
    {
        public DeleteModelSizeCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(DeleteModelSizeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var modelSizeRepository = UnitOfWork.GetRepositoryOf<ModelSize>();
                await modelSizeRepository.RemoveAsync(request.ModelSizeId, cancellationToken);
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
