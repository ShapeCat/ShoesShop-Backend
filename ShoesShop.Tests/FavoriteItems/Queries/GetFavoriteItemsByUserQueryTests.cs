using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.FavoriteItems.OutputVMs;
using ShoesShop.Application.Requests.FavoriteItems.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.FavoriteItems.Queries
{
    public class GetFavoriteItemsByUserQueryTests : AbstractQueryTests
    {
        public GetFavoriteItemsByUserQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async void Should_ReturnUSerFavoriteItems_WhenCorrect()
        {
            var command = new GetFavoriteItemsByUserQuery()
            {
                UserId = TestData.UpdateUserId,
            };
            var handler = new GetFavoriteItemsByUserQueryHandler(UnitOfWork, Mapper);

            var userFavorItems = await handler.Handle(command, CancellationToken.None);

            userFavorItems.ShouldAllBe(x => x is FavoriteItemVm);
        }

        [Fact]
        public async void Should_ThrowException_WhenUserNotExists()
        {
            var command = new GetFavoriteItemsByUserQuery()
            {
                UserId = Guid.NewGuid(),
            };
            var handler = new GetFavoriteItemsByUserQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
