using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record DeleteModelCommand : IRequest<Unit>
    {
        public Guid ModelId { get; set; }
    }

    public class DeleteModelCommandHandler : AbstractCommandHandler<DeleteModelCommand, Unit>
    {
        public DeleteModelCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
                await modelRepository.RemoveAsync(request.ModelId, cancellationToken);
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
