using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Application.Requests.Images.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Images.Commands
{
    public class UpdateImageCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_UpdateImage_WhenImageExists()
        {
            // Arrange
            var newImage = new Image()
            {
                Url = "update test url"
            };
            var command = new UpdateImageCommand()
            {
                ImageId = TestData.UpdateImageId,
                Url = newImage.Url,
                IsPreview = newImage.IsPreview,
            };
            var handler = new UpdateImageCommandhandler(UnitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            DbContext.Images.FirstOrDefault(x => x.ImageId == TestData.UpdateImageId
                                                   && x.Url == newImage.Url
                                                   && x.IsPreview == newImage.IsPreview).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenImageNotExists()
        {
            // Arrange
            var newImage = new Image()
            {
                Url = "update test url"
            };
            var command = new UpdateImageCommand()
            {
                ImageId = Guid.NewGuid(),
                Url = newImage.Url,
                IsPreview = newImage.IsPreview,
            };
            var handler = new UpdateImageCommandhandler(UnitOfWork);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
