using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Addresses.Commands
{
    public record CreateAddressCommand : IRequest<Guid>
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Room { get; set; }
    }

    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.Country).NotEmpty().MaximumLength(128);
            RuleFor(x => x.City).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Street).NotEmpty().MaximumLength(128);
            RuleFor(x => x.House).NotEmpty().MaximumLength(128);
        }
    }

    public class CreateAddressCommandHandler : AbstractCommandHandler<CreateAddressCommand, Guid>
    {
        public CreateAddressCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var addressRepository = UnitOfWork.GetRepositoryOf<Address>();
            var address = new Address(request.Country, request.City, request.Street, request.House, room:request.Room);
            await addressRepository.AddAsync(address, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return address.AddressId;
        }
    }
}