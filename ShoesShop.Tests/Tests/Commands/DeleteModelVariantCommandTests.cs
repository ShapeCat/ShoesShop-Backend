using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class DeleteModelVariantCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_DeleteModelVariant_WhenModelVariantExists()
        {
            var command = new DeleteModelVariantCommand()
            {
                ModelvariantId = TestData.DeleteModelVariantId
            };
            var handler = new DeleteModelVariantCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert 
            DbContext.ModelsVariants.FirstOrDefault(x => x.ModelVariantId == TestData.DeleteModelVariantId).ShouldBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenModelVariantNotExists()
        {
            var command = new DeleteModelVariantCommand()
            {
                ModelvariantId = Guid.NewGuid(),
            };
            var handler = new DeleteModelVariantCommandHandler(UnitOfWork);

            // Act
            // Assert 
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
