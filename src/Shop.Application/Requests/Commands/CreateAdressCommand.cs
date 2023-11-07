using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record CreateAdressCommand : IRequest<Guid>
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Room { get; set; }
    }

    public class CreateAdressCommandHandler : AbstractCommandHandler, IRequestHandler<CreateAdressCommand, Guid>
    {
        public CreateAdressCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<Guid> Handle(CreateAdressCommand request, CancellationToken cancellationToken)
        {
            var adressRepository = unitOfWork.GetRepositoryOf<Adress>(true);
            var adress = new Adress()
            {
                Country = request.Country,
                City = request.City,
                Street = request.Street,
                House = request.House,
                Room = request.Room
            };

            await adressRepository.AddAsync(adress, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var createdAdress = await adressRepository.FindAllAsync(x => x.Country == adress.Country
                                                                     && x.City == adress.City
                                                                     && x.Street == adress.Street
                                                                     && x.House == adress.House
                                                                     && x.Room == adress.Room, cancellationToken);
            return createdAdress.First().Id;
        }
    }
}