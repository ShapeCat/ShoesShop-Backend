using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.FavoriteItems.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.FavoriteItems.Commands
{
    public class DeleteFavoriteItemCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_DeleteFavoriteItem_WhenExists()
        {
            var command = new DeleteFavoriteItemCommand()
            {
                FavoriteItemId = TestData.DeleteFavoriteItemId,
            };
            var handler = new DeleteFavoriteItemCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.FavoritesItems.FirstOrDefault(x => x.FavoriteItemId == command.FavoriteItemId).ShouldBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenFavoriteItemNotExists()
        {
            var command = new DeleteFavoriteItemCommand()
            {
                FavoriteItemId = Guid.NewGuid(),
            };
            var handler = new DeleteFavoriteItemCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
