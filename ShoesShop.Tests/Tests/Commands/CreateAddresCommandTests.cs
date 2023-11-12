using ShoesShop.Application.Requests.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class CreateAddresCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_CreateAdress_WhenAdressNotExists()
        {
            //Arrange
            var adressToCreate = new Adress()
            {
                Country = "add test country",
                City = "add test city",
                Street = "add test street",
                House = "add test house",
            };
            var command = new CreateAdressCommand()
            {
                Country = adressToCreate.Country,
                City = adressToCreate.City,
                Street = adressToCreate.Street,
                House = adressToCreate.House,
                Room = adressToCreate.Room
            };
            var handler = new CreateAdressCommandHandler(UnitOfWork);

            //Act 
            var createdAdressId = await handler.Handle(command, CancellationToken.None);

            //Assert
            DbContext.Adresses.SingleOrDefault(x => x.Id == createdAdressId
                                                    && x.Country == adressToCreate.Country
                                                    && x.City == adressToCreate.City
                                                    && x.Street == x.Street
                                                    && x.House == x.House
                                                    && x.Room == x.Room).ShouldNotBeNull();
        }
    }
}
