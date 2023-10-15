using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record UpdateShoesSizeCommand : IRequest<Unit>
    {
        public Guid ShoesSizeId { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public int ItemsLeft { get; set; }
    }

    public class UpdateShoesSizeCommandhandler : IRequestHandler<UpdateShoesSizeCommand, Unit>
    {
        private readonly IShoesSizeRepository shoesSizeRepository;

        public UpdateShoesSizeCommandhandler(IShoesSizeRepository shoesSizeRepository) => this.shoesSizeRepository = shoesSizeRepository;

        public async Task<Unit> Handle(UpdateShoesSizeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shoesSize = new ShoesSize()
                {
                    Size = request.Size,
                    Price = request.Price,
                    ItemsLeft = request.ItemsLeft
                };
                await shoesSizeRepository.EditAsync(request.ShoesSizeId, shoesSize, cancellationToken);
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
