using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class DeleteAdressCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_DeleteAdress_WhenAdressExists()
        {
            // Arrange
            var command = new DeleteAdressCommand()
            {
                AdressId = TestData.DeleteAdressId
            };
            var handler = new DeleteAdressCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert 
            DbContext.Adresses.FirstOrDefault(x => x.Id == TestData.DeleteAdressId).ShouldBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenAdressNotExists()
        {
            // Arrange
            var command = new DeleteAdressCommand()
            {
                AdressId = Guid.NewGuid(),
            };
            var handler = new DeleteAdressCommandHandler(UnitOfWork);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
