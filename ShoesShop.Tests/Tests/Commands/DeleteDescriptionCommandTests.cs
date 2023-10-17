using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Persistence.Repository;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class DeleteDescriptionCommandTests : CommandTestAbstract
    {
        private readonly IDescriptionRepository descriptionRepository;

        public DeleteDescriptionCommandTests() => descriptionRepository = new DescriptionRepository(dbContext);

        [Fact]
        public async Task Should_DeleteDescriptions_WhenDescriptionExists()
        {
            // Arrange
            var command = new DeleteDescriptionCommand()
            {
                DescriptionId = ShoesShopTextContext.DescriptionToDelete
            };
            var handler = new DeleteDescriptionCommandHandler(descriptionRepository);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert 
            dbContext.Descriptions.FirstOrDefault(x => x.Id == ShoesShopTextContext.DescriptionToDelete).ShouldBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenDescriptionNotExists()
        {
            // Arrange
            var command = new DeleteDescriptionCommand()
            {
                DescriptionId = Guid.NewGuid(),
            };
            var handler = new DeleteDescriptionCommandHandler(descriptionRepository);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
