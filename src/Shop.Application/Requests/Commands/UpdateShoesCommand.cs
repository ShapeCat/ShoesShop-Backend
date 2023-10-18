using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;

namespace ShoesShop.Application.Requests.Commands
{
    public record UpdateShoesCommand : IRequest<Unit>
    {
        public Guid ShoesId { get; set; }
        public string Name { get; set; }
    }

    public class UpdateShoesNameCommandHandler : IRequestHandler<UpdateShoesCommand, Unit>
    {
        private readonly IShoesRepository shoesRepository;

        public UpdateShoesNameCommandHandler(IShoesRepository shoesRepository) => this.shoesRepository = shoesRepository;

        public async Task<Unit> Handle(UpdateShoesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await shoesRepository.EditAsync(request.ShoesId, request.Name, cancellationToken);
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
