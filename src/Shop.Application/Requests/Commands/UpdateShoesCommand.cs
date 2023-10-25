using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Entities;

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
                var newShoes = new Shoes()
                {
                    Id = request.ShoesId,
                    Name = request.Name,
                };
                await shoesRepository.EditAsync(newShoes, cancellationToken);
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
