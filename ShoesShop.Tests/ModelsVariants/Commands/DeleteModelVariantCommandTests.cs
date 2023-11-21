using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ModelsVariants.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsVariants.Commands
{
    public class DeleteModelVariantCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_DeleteModelVariant_WhenCorrect()
        {
            var command = new DeleteModelVariantCommand()
            {
                ModelVariantId = TestData.DeleteModelVariantId
            };
            var handler = new DeleteModelVariantCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.ModelsVariants.FirstOrDefault(x => x.ModelVariantId == TestData.DeleteModelVariantId).ShouldBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenModelVariantNotExists()
        {
            var command = new DeleteModelVariantCommand()
            {
                ModelVariantId = Guid.NewGuid(),
            };
            var handler = new DeleteModelVariantCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
