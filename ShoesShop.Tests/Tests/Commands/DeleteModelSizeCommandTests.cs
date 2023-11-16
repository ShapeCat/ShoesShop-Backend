using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class DeleteModelSizeCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_DeleteModelSize_WhenModelSizeExists()
        {
            var command = new DeleteModelSizeCommand()
            {
                ModelSizeId = TestData.DeleteModelSizeId
            };
            var handler = new DeleteModelSizeCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert 
            DbContext.ModelsSizes.FirstOrDefault(x => x.Id == TestData.DeleteModelSizeId).ShouldBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenModelSizeNotExists()
        {
            var command = new DeleteModelSizeCommand()
            {
                ModelSizeId = Guid.NewGuid(),
            };
            var handler = new DeleteModelSizeCommandHandler(UnitOfWork);

            // Act
            // Assert 
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
