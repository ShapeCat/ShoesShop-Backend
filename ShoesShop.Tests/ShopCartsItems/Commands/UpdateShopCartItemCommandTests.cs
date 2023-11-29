using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ShopCartsItems.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ShopCartsItems.Commands
{
    public class UpdateShopCartItemCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_ThrowException_WhenShopCartItemNotExists()
        {
            var command = new UpdateShopCartItemCommand()
            {
                ShopCartItemId = Guid.NewGuid(),
                Amount = 1,
            };
            var handler = new UpdateShopCartItemCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void Should_UpdateShopCartItem_WhenCorrect()
        {
            var command = new UpdateShopCartItemCommand()
            {
                ShopCartItemId = TestData.UpdateCartItemId,
                Amount = 1,
            };
            var handler = new UpdateShopCartItemCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            await DbContext.ShopCartsItems.SingleOrDefaultAsync(x => x.ShopCartItemId == command.ShopCartItemId
                                                          && x.Amount == command.Amount).ShouldNotBeNull();
        }
    }
}
