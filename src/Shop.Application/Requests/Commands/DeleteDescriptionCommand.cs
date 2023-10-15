using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;

namespace ShoesShop.Application.Requests.Commands
{
    public record DeleteDescriptionCommand : IRequest<Unit>
    {
        public Guid DescriptionId { get; set; }
    }

    public class DeleteDescriptionCommandHandler : IRequestHandler<DeleteDescriptionCommand, Unit>
    {
        private readonly IDescriptionRepository descriptionRepository;

        public DeleteDescriptionCommandHandler(IDescriptionRepository descriptionRepository) => this.descriptionRepository = descriptionRepository;

        public async Task<Unit> Handle(DeleteDescriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await descriptionRepository.RemoveAsync(request.DescriptionId, cancellationToken);
                await descriptionRepository.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
