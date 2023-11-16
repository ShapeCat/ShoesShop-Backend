using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Adresses.Commands;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Addresses.Commands
{
    public class DeleteAddressCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_DeleteAddress_WhenAddressExists()
        {
            // Arrange
            var command = new DeleteAddressCommand()
            {
                AddressId = TestData.DeleteAddressId
            };
            var handler = new DeleteAddressCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert 
            DbContext.Addresses.FirstOrDefault(x => x.AddressId == TestData.DeleteAddressId).ShouldBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenAddressNotExists()
        {
            // Arrange
            var command = new DeleteAddressCommand()
            {
                AddressId = Guid.NewGuid(),
            };
            var handler = new DeleteAddressCommandHandler(UnitOfWork);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
