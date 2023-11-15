using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class UpdateModelVariantCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_UpdateModelVariant_WhenModelvariantExists()
        {
            // Arrange
            var modelVariantToUpdate = new ModelVariant()
            {
                Id = TestData.UpdateModelVariantId,
                ItemsLeft = 0,
            };
            var command = new UpdateModelVariantCommand()
            {
                ModelVariantId = modelVariantToUpdate.Id,
                ItemsLeft = modelVariantToUpdate.ItemsLeft,

            };
            var handler = new UpdateModelVariantCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            DbContext.ModelsVariants.SingleOrDefault(x => x.Id == TestData.UpdateModelVariantId
                                                          && x.ItemsLeft == modelVariantToUpdate.ItemsLeft).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelVariantNotExists()
        {
            // Arrange
            var modelVariantToUpdate = new ModelVariant()
            {
                Id = Guid.NewGuid(),
                ItemsLeft = 0,
            };
            var command = new UpdateModelVariantCommand()
            {
                ModelVariantId = modelVariantToUpdate.Id,
                ItemsLeft = modelVariantToUpdate.ItemsLeft,

            };
            var handler = new UpdateModelVariantCommandHandler(UnitOfWork);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
