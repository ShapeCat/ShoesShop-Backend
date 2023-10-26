using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Entities;
using ShoesShop.Persistence.Repository;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class UpdateDescriptionCommandTests : AbstractCommandTest
    {
        [Fact]
        public async Task Should_UpdateDescription_WhenDescriptionExists()
        {
            // Arrange
            var newDescription = new Description()
            {
                SkuID = "NewSkuID",
                ReleaseDate = new DateTime(1990, 1, 1),
                ColorName = "NewColorname"
            };
            var command = new UpdateDescriptionCommand()
            {
                DescriptionId = ShoesShopTestContext.DescriptionToUpdate,
                SkuID = newDescription.SkuID,
                ReleaseDate = newDescription.ReleaseDate,
                ColorName = newDescription.ColorName
            };
            var handler = new UpdateDescriptionCommandHandler(unitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            dbContext.Descriptions.FirstOrDefault(x => x.Id == ShoesShopTestContext.DescriptionToUpdate
                                                    && x.SkuID == newDescription.SkuID
                                                    && x.ReleaseDate == newDescription.ReleaseDate
                                                    && x.ColorName == newDescription.ColorName).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenDescriptionNotExists()
        {
            // Arrange
            var newDescription = new Description()
            {
                SkuID = "NewSkuID",
                ReleaseDate = new DateTime(1990, 1, 1),
                ColorName = "NewColorname"
            };
            var command = new UpdateDescriptionCommand()
            {
                DescriptionId = Guid.NewGuid(),
                SkuID = newDescription.SkuID,
                ReleaseDate = newDescription.ReleaseDate,
                ColorName = newDescription.ColorName
            };
            var handler = new UpdateDescriptionCommandHandler(unitOfWork);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
