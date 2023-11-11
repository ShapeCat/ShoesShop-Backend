using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record DeleteImageCommand : IRequest<Unit>
    {
        public Guid ImageId { get; set; }
    }

    public class DeleteImageCommandHandler : AbstractCommandHandler<DeleteImageCommand, Unit>
    {
        public DeleteImageCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var imageRepository = unitOfWork.GetRepositoryOf<Image>(true);
                await imageRepository.RemoveAsync(request.ImageId, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
