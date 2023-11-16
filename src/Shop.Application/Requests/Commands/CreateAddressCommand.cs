using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record CreateAddressCommand : IRequest<Guid>
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Room { get; set; }
    }

    public class CreateAddressCommandHandler : AbstractCommandHandler<CreateAddressCommand, Guid>
    {
        public CreateAddressCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {            
            var addressRepository = UnitOfWork.GetRepositoryOf<Address>(true);
            var address = new Address()
            {
                Country = request.Country,
                City = request.City,
                Street = request.Street,
                House = request.House,
                Room = request.Room
            };

            await addressRepository.AddAsync(address, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            var createddAddress = await addressRepository.FindAllAsync(x => x.Country == address.Country
                                                                     && x.City == address.City
                                                                     && x.Street == address.Street
                                                                     && x.House == address.House
                                                                     && x.Room == address.Room, cancellationToken);
            return createddAddress.First().Id;
        }
    }
}