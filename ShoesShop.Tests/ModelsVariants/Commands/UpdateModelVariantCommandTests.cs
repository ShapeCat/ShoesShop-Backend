using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ModelsVariants.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsVariants.Commands
{
    public class UpdateModelVariantCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_UpdateModelVariant_WhenCorrect()
        {
            var command = new UpdateModelVariantCommand()
            {
                ModelVariantId = TestData.UpdateModelVariantId,
                ItemsLeft = 0,

            };
            var handler = new UpdateModelVariantCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.ModelsVariants.SingleOrDefault(x => x.ModelVariantId == TestData.UpdateModelVariantId
                                                          && x.ItemsLeft == command.ItemsLeft).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelVariantNotExists()
        {
            var command = new UpdateModelVariantCommand()
            {
                ModelVariantId = Guid.NewGuid(),
                ItemsLeft = 0,
            };
            var handler = new UpdateModelVariantCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
