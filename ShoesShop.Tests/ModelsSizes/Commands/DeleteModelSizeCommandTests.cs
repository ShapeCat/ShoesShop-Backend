using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ModelsSizes.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsSizes.Commands
{
    public class DeleteModelSizeCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_DeleteModelSize_WhenCorrect()
        {
            var command = new DeleteModelSizeCommand()
            {
                ModelSizeId = TestData.DeleteModelSizeId
            };
            var handler = new DeleteModelSizeCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.ModelsSizes.FirstOrDefault(x => x.ModelSizeId == TestData.DeleteModelSizeId).ShouldBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenModelSizeNotExists()
        {
            var command = new DeleteModelSizeCommand()
            {
                ModelSizeId = Guid.NewGuid(),
            };
            var handler = new DeleteModelSizeCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
