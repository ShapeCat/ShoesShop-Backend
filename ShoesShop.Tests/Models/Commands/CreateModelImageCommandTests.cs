using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Models.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Models.Commands
{
    public class CreateModelImageCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_CreateImage_WhenCorrect()
        {
            var command = new CreateModelImageCommand()
            {
                ModelId = TestData.UpdateModelId,
                Url = "add test url",
                IsPreview = true,
            };
            var handler = new CreateModelImageCommandHandler(UnitOfWork);

            var createdImageId = await handler.Handle(command, CancellationToken.None);

            DbContext.Images.SingleOrDefault(x => x.ImageId == createdImageId
                                                  && x.Url == command.Url
                                                  && x.ModelId == command.ModelId
                                                  && x.IsPreview == command.IsPreview).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelNotExists()
        {
            var command = new CreateModelImageCommand()
            {
                ModelId = Guid.NewGuid(),
                Url = "add test url",
                IsPreview = true,
            };
            var handler = new CreateModelImageCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
