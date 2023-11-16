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
        public async Task Should_CreateAddress_WhenAddressNotExists()
        {
            //Arrange
            var addressToCreate = new Address()
            {
                Country = "add test country",
                City = "add test city",
                Street = "add test street",
                House = "add test house",
            };
            var command = new CreateAddressCommand()
            {
                Country = addressToCreate.Country,
                City = addressToCreate.City,
                Street = addressToCreate.Street,
                House = addressToCreate.House,
                Room = addressToCreate.Room
            };
            var handler = new CreateAddressCommandHandler(UnitOfWork);

            //Act 
            var createdAddressId = await handler.Handle(command, CancellationToken.None);

            //Assert
            DbContext.Addresses.SingleOrDefault(x => x.AddressId == createdAddressId
                                                    && x.Country == addressToCreate.Country
                                                    && x.City == addressToCreate.City
                                                    && x.Street == x.Street
                                                    && x.House == x.House
                                                    && x.Room == x.Room).ShouldNotBeNull();
        }
    }
}
