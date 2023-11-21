using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Addresses.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Addresses.Commands
{
    public class UpdateAddressCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_UpdateAddress_WhenCorrect()
        {
            var newAddress = new Address()
            {
                AddressId = TestData.UpdateAddressId,
                Country = "edit test country",
                City = "edit test city",
                Street = "edit test street",
                House = "edit test house",
                Room = 1,
            };
            var command = new UpdateAddressCommand()
            {
                AddressId = newAddress.AddressId,
                Country = newAddress.Country,
                City = newAddress.City,
                Street = newAddress.Street,
                House = newAddress.House,
                Room = newAddress.Room,
            };
            var handler = new UpdateAddressCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.Addresses.FirstOrDefault(x => x.AddressId == newAddress.AddressId
                                                   && x.Country == newAddress.Country
                                                   && x.City == newAddress.City
                                                   && x.Street == newAddress.Street
                                                   && x.House == newAddress.House
                                                   && x.Room == newAddress.Room).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenAddressNotExists()
        {
            var newAddress = new Address()
            {
                AddressId = Guid.NewGuid(),
                Country = "edit test country",
                City = "edit test city",
                Street = "edit test street",
                House = "edit test house",
                Room = 1,
            };
            var command = new UpdateAddressCommand()
            {
                AddressId = newAddress.AddressId,
                Country = newAddress.Country,
                City = newAddress.City,
                Street = newAddress.Street,
                House = newAddress.House,
                Room = newAddress.Room,
            };
            var handler = new UpdateAddressCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
