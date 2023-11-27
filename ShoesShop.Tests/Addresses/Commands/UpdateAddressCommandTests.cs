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
            var command = new UpdateAddressCommand()
            {
                AddressId = TestData.UpdateAddressId,
                Country = "edit test country",
                City = "edit test city",
                Street = "edit test street",
                House = "1",
            };
            var handler = new UpdateAddressCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.Addresses.FirstOrDefault(x => x.AddressId == command.AddressId
                                                   && x.Country == command.Country
                                                   && x.City == command.City
                                                   && x.Street == command.Street
                                                   && x.House == command.House
                                                   && x.Room == command.Room).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenAddressNotExists()
        {
            var command = new UpdateAddressCommand()
            {
                AddressId = Guid.NewGuid(),
                Country = "edit test country",
                City = "edit test city",
                Street = "edit test street",
                House = "1",
            };
            var handler = new UpdateAddressCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
