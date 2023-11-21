using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Images.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Images.Commands
{
    public class DeleteImageCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_DeleteImage_WhenCorrect()
        {
            var command = new DeleteImageCommand()
            {
                ImageId = TestData.DeleteImageId
            };
            var handler = new DeleteImageCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.Images.FirstOrDefault(x => x.ImageId == TestData.DeleteAddressId).ShouldBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenImageNotExists()
        {
            var command = new DeleteImageCommand()
            {
                ImageId = Guid.NewGuid(),
            };
            var handler = new DeleteImageCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
