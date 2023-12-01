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
                UserId = TestData.DeleteUserId,
                ModelVariantId = TestData.DeleteModelVariantId
            };
            var handler = new DeleteShopCartItemCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.ShopCartsItems.FirstOrDefault(x => x.UserId == command.UserId
                                                         && x.ModeVariantId == command.ModelVariantId).ShouldBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenUserNotExists()
        {
            var command = new DeleteShopCartItemCommand()
            {
                UserId = Guid.NewGuid(),
                ModelVariantId = TestData.DeleteModelVariantId
            };
            var handler = new DeleteShopCartItemCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void Should_ThrowException_WhenModelVariantNotExists()
        {
            var command = new DeleteShopCartItemCommand()
            {
                UserId = TestData.DeleteUserId,
                ModelVariantId = Guid.NewGuid()
            };
            var handler = new DeleteShopCartItemCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
