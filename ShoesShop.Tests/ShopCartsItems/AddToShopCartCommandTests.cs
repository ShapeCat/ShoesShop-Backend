using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ShopCartsItems.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ShopCartsItems
{
    public class AddToShopCartCommandTests : AbstractCommandTests
    {

        [Fact]
        public async void Should_CreateShopCartItem_WhenCorrect()
        {
            var command = new AddToShopCartCommand()
            {
                UserId = TestData.UpdateUserId,
                ModelVariantId = TestData.DeleteModelVariantId,
                Amount = 1,
            };
            var handler = new AddToShopCartCommandHandler(UnitOfWork);

            var shopCartItemId = await handler.Handle(command, CancellationToken.None);

            DbContext.ShopCartsItems.FirstOrDefault(x => x.ShopCartItemId == shopCartItemId
                                                         && x.ModeVariantId == command.ModelVariantId
                                                         && x.UserId == command.UserId
                                                         && x.Amount == command.Amount).ShouldNotBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenUserNotExists()
        {
            var command = new AddToShopCartCommand()
            {
                UserId = TestData.UpdateUserId,
                ModelVariantId = Guid.NewGuid(),
                Amount = 1,
            };
            var handler = new AddToShopCartCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void Should_ThrowException_WhenModelVariantNotExists()
        {
            var command = new AddToShopCartCommand()
            {
                UserId = Guid.NewGuid(),
                ModelVariantId = TestData.DeleteModelVariantId,
                Amount = 1,
            };
            var handler = new AddToShopCartCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async void Should_ThrowException_WhenSameItemExists()
        {
            var command = new AddToShopCartCommand()
            {
                UserId = TestData.UpdateUserId,
                ModelVariantId = TestData.UpdateModelVariantId,
                Amount = 1,
            };
            var handler = new AddToShopCartCommandHandler(UnitOfWork);

            await Should.ThrowAsync<AlreadyExistsException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
