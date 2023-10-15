using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;

namespace ShoesShop.Application.Requests.Commands
{
    public record DeleteShoesCommand : IRequest<Unit>
    {
        public Guid ShoesId { get; set; }
    }

    public class DeleteShoesCommandHandler : IRequestHandler<DeleteShoesCommand, Unit>
    {
        private readonly IShoesRepository shoesRepository;

        public DeleteShoesCommandHandler(IShoesRepository shoesRepository) => this.shoesRepository = shoesRepository;

        public async Task<Unit> Handle(DeleteShoesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await shoesRepository.RemoveAsync(request.ShoesId, cancellationToken);
                await shoesRepository.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
