using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.FavoriteItems.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.FavoriteItems.Commands
{
    public class CreateFavoriteItemCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_CreateFavoriteItem_WhenCorrect()
        {
            var command = new CreateFavoriteItemCommand()
            {
                UserId = TestData.UpdateUserId,
                ModelVariantId = TestData.DeleteModelVariantId,
            };
            var handler = new CreateFavoriteItemCommandHandler(UnitOfWork);

            var createdFavoriteItemId = await handler.Handle(command, CancellationToken.None);

            DbContext.FavoritesItems.SingleOrDefault(x => x.FavoriteItemId == createdFavoriteItemId
                                                          && x.UserId == command.UserId
                                                          && x.ModelVariantId == command.ModelVariantId).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelVariantNotExists()
        {
            var command = new CreateFavoriteItemCommand()
            {
                UserId = Guid.NewGuid(),
                ModelVariantId = TestData.DeleteModelVariantId,
            };
            var handler = new CreateFavoriteItemCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Should_ThrowException_WhenUserNotExists()
        {
            var command = new CreateFavoriteItemCommand()
            {
                UserId = TestData.UpdateUserId,
                ModelVariantId = Guid.NewGuid(),
            };
            var handler = new CreateFavoriteItemCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Should_ThrowException_WhenFavoriteItemAlreadyExists()
        {
            var command = new CreateFavoriteItemCommand()
            {
                UserId = TestData.UpdateUserId,
                ModelVariantId = TestData.UpdateModelVariantId,
            };
            var handler = new CreateFavoriteItemCommandHandler(UnitOfWork);

            await Should.ThrowAsync<AlreadyExistsException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
