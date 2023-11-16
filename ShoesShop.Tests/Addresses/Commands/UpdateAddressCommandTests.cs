using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Addresses.Commands
{
    public class UpdateAddressCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_UpdateAddress_WhenAddressExists()
        {
            // Arrange
            var newAddress = new Address()
            {
                Country = "edit test country",
                City = "edit test city",
                Street = "edit test street",
                House = "edit test house",
                Room = 1,
            };
            var command = new UpdateAddressCommand()
            {
                AddressId = TestData.UpdateAddressId,
                Country = newAddress.Country,
                City = newAddress.City,
                Street = newAddress.Street,
                House = newAddress.House,
                Room = newAddress.Room,
            };
            var handler = new UpdateAddressCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            DbContext.Addresses.FirstOrDefault(x => x.AddressId == TestData.UpdateAddressId
                                                   && x.Country == newAddress.Country
                                                   && x.City == newAddress.City
                                                   && x.Street == newAddress.Street
                                                   && x.House == newAddress.House
                                                   && x.Room == newAddress.Room).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenAddressNotExists()
        {
            // Arrange
            var newAddress = new Address()
            {
                Country = "edit test country",
                City = "edit test city",
                Street = "edit test street",
                House = "edit test house",
                Room = 1,
            };
            var command = new UpdateAddressCommand()
            {
                AddressId = Guid.NewGuid(),
                Country = newAddress.Country,
                City = newAddress.City,
                Street = newAddress.Street,
                House = newAddress.House,
                Room = newAddress.Room,
            };
            var handler = new UpdateAddressCommandHandler(UnitOfWork);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
