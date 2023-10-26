using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record DeleteShoesSizeCommand : IRequest<Unit>
    {
        public Guid ShoesSizeId { get; set; }
    }

    public class DeleteShoesSizeCommandHandler : AbstractCommand, IRequestHandler<DeleteShoesSizeCommand, Unit>
    {
        public DeleteShoesSizeCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<Unit> Handle(DeleteShoesSizeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shoesSizesRepository = unitOfWork.GetRepositoryOf<ShoesSize>(true);
                await shoesSizesRepository.RemoveAsync(request.ShoesSizeId, cancellationToken);
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
