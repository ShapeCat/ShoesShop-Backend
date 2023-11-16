using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Application.Requests.Images.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Images.Commands
{
    public class DeleteImageCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_DeleteImage_WhenImageExists()
        {
            // Arrange
            var command = new DeleteImageCommand()
            {
                ImageId = TestData.DeleteImageId
            };
            var handler = new DeleteImageCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert 
            DbContext.Images.FirstOrDefault(x => x.ImageId == TestData.DeleteAddressId).ShouldBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenImageNotExists()
        {
            // Arrange
            var command = new DeleteImageCommand()
            {
                ImageId = Guid.NewGuid(),
            };
            var handler = new DeleteImageCommandHandler(UnitOfWork);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
