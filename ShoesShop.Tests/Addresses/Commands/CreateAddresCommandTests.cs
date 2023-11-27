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
            var command = new CreateAddressCommand()
            {
                Country = "add test country",
                City = "add test city",
                Street = "add test street",
                House = "add test house",
            };
            var handler = new CreateAddressCommandHandler(UnitOfWork);

            var createdAddressId = await handler.Handle(command, CancellationToken.None);

            DbContext.Addresses.SingleOrDefault(x => x.AddressId == createdAddressId
                                                    && x.Country == command.Country
                                                    && x.City == command.City
                                                    && x.Street == command.Street
                                                    && x.House == command.House
                                                    && x.Room == command.Room).ShouldNotBeNull();
        }
    }
}
