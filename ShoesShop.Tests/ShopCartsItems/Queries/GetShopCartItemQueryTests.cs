using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ShopCartsItems.OutputVMs;
using ShoesShop.Application.Requests.ShopCartsItems.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ShopCartsItems.Queries
{
    public class GetShopCartItemQueryTests : AbstractQueryTests
    {
        public GetShopCartItemQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnShopCartItem_WhenCorrect()
        {
            var query = new GetShopCartItemQuery()
            {
                ShopCartItemId = TestData.UpdateCartItemId,
            };
            var handler = new GetShopCartItemQueryHandler(UnitOfWork, Mapper);

            var shopCartItem = await handler.Handle(query, CancellationToken.None);

            shopCartItem.ShouldBeOfType<ShopCartItemVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenShopcartItemNotExists()
        {
            var query = new GetShopCartItemQuery()
            {
                ShopCartItemId = Guid.NewGuid(),
            };
            var handler = new GetShopCartItemQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }

    }
}
