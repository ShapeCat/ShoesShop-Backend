using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Adresses.Commands
{
    public record DeleteAddressCommand : IRequest<Unit>
    {
        public Guid AddressId { get; set; }
    }

    public class DeleteAddressCommandHandler : AbstractCommandHandler<DeleteAddressCommand, Unit>
    {
        public DeleteAddressCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var addressRepository = UnitOfWork.GetRepositoryOf<Address>();
                await addressRepository.RemoveAsync(request.AddressId, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException) { throw; }
        }
    }
}
