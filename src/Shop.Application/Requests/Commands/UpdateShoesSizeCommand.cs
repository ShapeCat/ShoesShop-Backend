using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
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

    public class UpdateShoesSizeCommandHandler : AbstractCommand, IRequestHandler<UpdateShoesSizeCommand, Unit>
    {
        public UpdateShoesSizeCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<Unit> Handle(UpdateShoesSizeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shoesSize = new ShoesSize()
                {
                    Id = request.ShoesSizeId,
                    Size = request.Size,
                    Price = request.Price,
                    ItemsLeft = request.ItemsLeft
                };
                var shoesSizesRepository = unitOfWork.GetRepositoryOf<ShoesSize>(true);
                await shoesSizesRepository.EditAsync(shoesSize, cancellationToken);
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
