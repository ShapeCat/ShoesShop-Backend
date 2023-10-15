using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record CreateShoesCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }

    public class CreateShoesCommandHandler : IRequestHandler<CreateShoesCommand, Guid>
    {
        private readonly IShoesRepository shoesRepository;

        public CreateShoesCommandHandler(IShoesRepository shoesRepository) => this.shoesRepository = shoesRepository;

        public async Task<Guid> Handle(CreateShoesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shoes = new Shoes()
                {
                    Name = request.Name
                };
                await shoesRepository.AddAsync(shoes, cancellationToken);
                await shoesRepository.SaveChangesAsync(cancellationToken);
                var output = await shoesRepository.GetByNameAsync(shoes.Name, cancellationToken);
                return output.Id;
            }
            catch (AlreadyExistsException ex)
            {
                throw ex;
            }
        }
    }
}
