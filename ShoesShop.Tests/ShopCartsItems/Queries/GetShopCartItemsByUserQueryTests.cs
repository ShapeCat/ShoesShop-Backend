using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ShopCartsItems.OutputVMs;
using ShoesShop.Application.Requests.ShopCartsItems.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ShopCartsItems.Queries
{
    public class GetShopCartItemsByUserQueryTests : AbstractQueryTests
    {
        public GetShopCartItemsByUserQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnShopCartItems_WhenCorrect()
        {
            var query = new GetShopCartItemsByUserQuery()
            {
                UserId = TestData.UpdateUserId,
            };
            var handler = new GetShopCartItemsByUserQueryHandler(UnitOfWork, Mapper);

            var shopCartItems = await handler.Handle(query, CancellationToken.None);

            shopCartItems.ShouldAllBe(x => x is ShopCartItemVm);
        }

        [Fact]
        public async Task Should_ThrowException_WhenUserNotExists()
        {
            var query = new GetShopCartItemsByUserQuery()
            {
                UserId = Guid.NewGuid(),
            };
            var handler = new GetShopCartItemsByUserQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
