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
    public class UpdateShoesSuzeCommandTests : CommandTestAbstract
    {
        public readonly IShoesSizeRepository shoesSizeRepository;

        public UpdateShoesSuzeCommandTests() => shoesSizeRepository = new ShoesSizeRepository(dbContext);

        [Fact]
        public async Task Should_UpdateShoesSize_WhenShoesSizeExists()
        {
            // Arrange
            var newSize = new ShoesSize()
            {
                Size = 2,
                Price = 2,
                ItemsLeft = 4,
            };
            var command = new UpdateShoesSizeCommand()
            {
                ShoesSizeId = ShoesShopTextContext.ShoesSizeToUpdate,
                Size = newSize.Size,
                Price = newSize.Price,
                ItemsLeft = newSize.ItemsLeft,
            };
            var handler = new UpdateShoesSizeCommandHandler(shoesSizeRepository);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert 
            dbContext.Sizes.SingleOrDefault(x => x.Id == ShoesShopTextContext.ShoesSizeToUpdate
                                                                && x.Size == newSize.Size
                                                                && x.Price == newSize.Price
                                                                && x.ItemsLeft == newSize.ItemsLeft).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenShoesSizeNotExists()
        {
            // Arrange
            var newSize = new ShoesSize()
            {
                Size = 2,
                Price = 2,
                ItemsLeft = 4,
            };
            var command = new UpdateShoesSizeCommand()
            {
                ShoesSizeId = Guid.NewGuid(),
                Size = newSize.Size,
                Price = newSize.Price,
                ItemsLeft = newSize.ItemsLeft,
            };
            var handler = new UpdateShoesSizeCommandHandler(shoesSizeRepository);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
