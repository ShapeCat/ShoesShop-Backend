using ShoesShop.Application.Requests.Addresses.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Addresses.Commands
{
    public class CreateAddressCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_CreateAddress_WhenCorrect()
        {
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

            var createdAddressId = await handler.Handle(command, CancellationToken.None);

            DbContext.Addresses.SingleOrDefault(x => x.AddressId == createdAddressId
                                                    && x.Country == addressToCreate.Country
                                                    && x.City == addressToCreate.City
                                                    && x.Street == x.Street
                                                    && x.House == x.House
                                                    && x.Room == x.Room).ShouldNotBeNull();
        }
    }
}
