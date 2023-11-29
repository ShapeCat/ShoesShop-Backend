using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ShopCartsItems.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ShopCartsItems.Commands
{
    public class DeleteShopCartItemCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_DeleteShopCartItem_WhenCorrect()
        {
            var command = new DeleteShopCartItemCommand()
            {
                ShopCartItemId = TestData.DeleteCartItemId
            };
            var handler = new DeleteShopCartItemCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.Sales.FirstOrDefault(x => x.ModelVariantId == command.ShopCartItemId).ShouldBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenShopCartItemNotExists()
        {
            var command = new DeleteShopCartItemCommand()
            {
                ShopCartItemId = Guid.NewGuid(),
            };
            var handler = new DeleteShopCartItemCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
