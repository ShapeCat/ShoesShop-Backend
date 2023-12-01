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
        public async void Should_UpdateShopCartItem_WhenCorrect()
        {
            var command = new UpdateShopCartItemCommand()
            {
                UserId = TestData.UpdateUserId,
                ModelVariantId = TestData.UpdateModelVariantId,
                Amount = 1,
            };
            var handler = new UpdateShopCartItemCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            await DbContext.ShopCartsItems.SingleOrDefaultAsync(x => x.UserId == command.UserId
                                                                     && x.ModeVariantId == command.ModelVariantId
                                                                     && x.Amount == command.Amount).ShouldNotBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenUserNotExists()
        {
            var command = new UpdateShopCartItemCommand()
            {
                UserId = Guid.NewGuid(),
                ModelVariantId = TestData.UpdateModelVariantId,
                Amount = 1,
            };
            var handler = new UpdateShopCartItemCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void Should_ThrowException_WhenShopCartItemNotExists()
        {
            var command = new UpdateShopCartItemCommand()
            {
                UserId = TestData.UpdateUserId,
                ModelVariantId = Guid.NewGuid(),
                Amount = 1,
            };
            var handler = new UpdateShopCartItemCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
