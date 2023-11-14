using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class UpdateModelSizeCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_ThrowException_WhenModelSizeNotExists()
        {
            // Arrange
            var modelSizeToUpdate = new ModelSize()
            {
                Size = 100,
            };
            var command = new UpdateModelSizeCommand()
            {
                ModelSizeId = TestData.UpdateModelSizeId,
                Size = modelSizeToUpdate.Size,
            };
            var handler = new UpdateModelSizeCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            DbContext.ModelsSizes.SingleOrDefault(x => x.Id == TestData.UpdateModelSizeId
                                                       && x.Size == modelSizeToUpdate.Size).ShouldNotBeNull();

        }

        [Fact]
        public async void Should_ThrrowException_WhenModelSizeNotExists()
        {
            // Arrange
            var modelSizeToUpdate = new ModelSize()
            {
                Size = 100,
            };
            var command = new UpdateModelSizeCommand()
            {
                ModelSizeId = Guid.NewGuid(),
                Size = modelSizeToUpdate.Size,
            };
            var handler = new UpdateModelSizeCommandHandler(UnitOfWork);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void Should_ThrowException_WhenSameModelSizeExists()
        {
            // Arrange
            var modelSizeToUpdate = new ModelSize()
            {
                Size = TestData.ExistedModelSize,
            };
            var command = new UpdateModelSizeCommand()
            {
                ModelSizeId = Guid.NewGuid(),
                Size = modelSizeToUpdate.Size,
            };
            var handler = new UpdateModelSizeCommandHandler(UnitOfWork);

            // Act
            // Assert
            await Should.ThrowAsync<AlreadyExistsException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
