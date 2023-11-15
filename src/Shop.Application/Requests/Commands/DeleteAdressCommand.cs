using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record DeleteAdressCommand : IRequest<Unit>
    {
        public Guid AdressId { get; set; }
    }

    public class DeleteAdressCommandHandler : AbstractCommandHandler<DeleteAdressCommand, Unit>
    {
        public DeleteAdressCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(DeleteAdressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var adressRepository = UnitOfWork.GetRepositoryOf<Adress>(true);
                await adressRepository.RemoveAsync(request.AdressId, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
