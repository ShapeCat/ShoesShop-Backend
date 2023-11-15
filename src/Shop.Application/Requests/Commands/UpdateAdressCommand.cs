using MediatR;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record UpdateAdressCommand : IRequest<Unit>
    {
        public Guid AdressId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Room { get; set; }
    }

    public class UpdateAdressCommandHandler : AbstractCommandHandler<UpdateAdressCommand, Unit>
    {
        public UpdateAdressCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(UpdateAdressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var adressRepository = UnitOfWork.GetRepositoryOf<Adress>(true);
                var newAdress = new Adress()
                {
                    Id = request.AdressId,
                    Country = request.Country,
                    City = request.City,
                    Street = request.Street,
                    House = request.House,
                    Room = request.Room,
                };

                await adressRepository.EditAsync(newAdress, cancellationToken);
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
