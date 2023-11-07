using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;

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
            var adress = new AdressVm()
            {
                Country = request.Country,
                City = request.City,
                Street = request.Street,
                House = request.House,
                Room = request.Room
            };
            var adressRepository = unitOfWork.GetRepositoryOf<AdressVm>();

            await adressRepository.AddAsync(adress, cancellationToken);
            var createdAdress = await adressRepository.FindAllAsync(x => x.Country == adress.City
                                                                     && x.City == adress.City
                                                                     && x.Street == adress.Street
                                                                     && x.House == adress.House
                                                                     && x.Room == adress.Room, cancellationToken);
            return createdAdress.First().Id;

        }
    }
}
