using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Addresses.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Addresses.Commands
{
    public class DeleteAddressCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_DeleteAddress_WhenCorrect()
        {
            var command = new DeleteAddressCommand()
            {
                AddressId = TestData.DeleteAddressId
            };
            var handler = new DeleteAddressCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.Addresses.FirstOrDefault(x => x.AddressId == TestData.DeleteAddressId).ShouldBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenAddressNotExists()
        {
            var command = new DeleteAddressCommand()
            {
                AddressId = Guid.NewGuid(),
            };
            var handler = new DeleteAddressCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
