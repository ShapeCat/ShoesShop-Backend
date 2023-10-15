using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;

namespace ShoesShop.Application.Requests.Commands
{
    public record DeleteShoesSizeCommand : IRequest<Unit>
    {
        public Guid ShoesSizeId { get; set; }
    }

    public class DeleteShoesSizeCommandHandler : IRequestHandler<DeleteShoesSizeCommand, Unit>
    {
        private readonly IShoesSizeRepository shoesSizeRepository;

        public DeleteShoesSizeCommandHandler(IShoesSizeRepository shoesSizeRepository) => this.shoesSizeRepository = shoesSizeRepository;

        public async Task<Unit> Handle(DeleteShoesSizeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await shoesSizeRepository.RemoveAsync(request.ShoesSizeId, cancellationToken);
                await shoesSizeRepository.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
