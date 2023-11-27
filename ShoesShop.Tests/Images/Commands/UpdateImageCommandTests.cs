using System;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Images.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Images.Commands
{
    public class UpdateImageCommandTests : AbstractCommandTests
    {
        [Fact]
        public async Task Should_UpdateImage_WhenCorrect()
        {
            var command = new UpdateImageCommand()
            {            
                ImageId = TestData.UpdateImageId,
                Url = "update test url"
            };
            var handler = new UpdateImageCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.Images.FirstOrDefault(x => x.ImageId == command.ImageId
                                                   && x.Url == command.Url
                                                   && x.IsPreview == command.IsPreview).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenImageNotExists()
        {
            var command = new UpdateImageCommand()
            {
                ImageId = Guid.NewGuid(),
                Url = "update test url"
            };
            var handler = new UpdateImageCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
