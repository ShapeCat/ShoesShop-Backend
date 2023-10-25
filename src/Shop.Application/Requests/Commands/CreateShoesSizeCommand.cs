using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record CreateShoesSizeCommand : IRequest<Guid>
    {
        public Guid ShoesId { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public int ItemsLeft { get; set; }
    }

    public class CreateShoesSizeCommandHandler : IRequestHandler<CreateShoesSizeCommand, Guid>
    {
        private readonly IShoesSizeRepository shoesSizeRepository;

        public CreateShoesSizeCommandHandler(IShoesSizeRepository shoesSizeRepository) => this.shoesSizeRepository = shoesSizeRepository;

        public async Task<Guid> Handle(CreateShoesSizeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shoesSize = new ShoesSize()
                {
                    ShoesId = request.ShoesId,
                    Size = request.Size,
                    Price = request.Price,
                    ItemsLeft = request.ItemsLeft
                };
                await shoesSizeRepository.AddAsync(shoesSize, cancellationToken);
                await shoesSizeRepository.SaveChangesAsync(cancellationToken);
                var output = await shoesSizeRepository.FindAllAsync(x => x.ShoesId == request.ShoesId, cancellationToken);
                return output.First().Id;
            }
            catch (AlreadyExistsException ex)
            {
                throw ex;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
