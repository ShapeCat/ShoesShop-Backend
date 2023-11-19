using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Adresses.Commands
{
    public record UpdateAddressCommand : IRequest<Unit>
    {
        public Guid AddressId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Room { get; set; }
    }

    public class UpdateAdressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAdressCommandValidator()
        {
            RuleFor(x => x.AddressId).NotEqual(Guid.Empty);
            RuleFor(x => x.Country).NotEmpty().MaximumLength(128);
            RuleFor(x => x.City).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Street).NotEmpty().MaximumLength(128);
            RuleFor(x => x.House).NotEmpty().MaximumLength(128);

        }
    }

    public class UpdateAddressCommandHandler : AbstractCommandHandler<UpdateAddressCommand, Unit>
    {
        public UpdateAddressCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var addressRepository = UnitOfWork.GetRepositoryOf<Address>();
                var newAddress = new Address()
                {
                    AddressId = request.AddressId,
                    Country = request.Country,
                    City = request.City,
                    Street = request.Street,
                    House = request.House,
                    Room = request.Room,
                };

                await addressRepository.EditAsync(newAddress, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException) { throw; }
        }
    }
}
