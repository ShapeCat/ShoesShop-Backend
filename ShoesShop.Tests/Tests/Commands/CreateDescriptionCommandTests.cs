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
    public class CreateDescriptionCommandTests : CommandTestAbstract
    {
        [Fact]
        public async Task Should_CreateDescription_WhenDescriptionNotExists()
        {
            //Arrange
            var descriptionToCreate = new Description()
            {
                SkuID = "TestSkuId",
                ColorName = "TestColor",
                ReleaseDate = new DateTime(2020, 1, 1)

            };
            var command = new CreateDescriptionCommand()
            {
                ShoesId = ShoesShopTextContext.EmptyShoes,
                SkuID = descriptionToCreate.SkuID,
                ColorName = descriptionToCreate.ColorName,
                ReleaseDate = descriptionToCreate.ReleaseDate,
            };
            var handler = new CreateDescriptionCommandHandler(unitOfWork);

            //Act 
            var descriptionId = await handler.Handle(command, CancellationToken.None);

            //Assert
            dbContext.Descriptions.SingleOrDefault(x => x.ShoesId == ShoesShopTextContext.EmptyShoes
                                                        && x.Id == descriptionId
                                                        && x.SkuID == descriptionToCreate.SkuID
                                                        && x.ColorName == descriptionToCreate.ColorName
                                                        && x.ReleaseDate == x.ReleaseDate).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenDescriptionAlreadyExists()
        {
            // Arrange
            var descriptionToCreate = new Description()
            {
                SkuID = "TestSkuId",
                ColorName = "TestColor",
                ReleaseDate = new DateTime(2020, 1, 1)

            };
            var command = new CreateDescriptionCommand()
            {
                ShoesId = ShoesShopTextContext.FullShoes,
                SkuID = descriptionToCreate.SkuID,
                ColorName = descriptionToCreate.ColorName,
                ReleaseDate = descriptionToCreate.ReleaseDate,
            };
            var handler = new CreateDescriptionCommandHandler(unitOfWork);

            // Act 
            // Assert
            await Should.ThrowAsync<AlreadyExistsException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Should_ThrowException_WhenShoesNotExists()
        {
            // Arrange
            var descriptionToCreate = new Description()
            {
                SkuID = "TestSkuId",
                ColorName = "TestColor",
                ReleaseDate = new DateTime(2020, 1, 1)

            };
            var command = new CreateDescriptionCommand()
            {
                ShoesId = Guid.NewGuid(),
                SkuID = descriptionToCreate.SkuID,
                ColorName = descriptionToCreate.ColorName,
                ReleaseDate = descriptionToCreate.ReleaseDate,
            };
            var handler = new CreateDescriptionCommandHandler(unitOfWork);

            // Act 
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
