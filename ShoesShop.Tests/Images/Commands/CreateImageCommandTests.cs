using ShoesShop.Application.Requests.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Images.Commands
{
    public class CreateImageCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_CreateImage_WhenImageNotExists()
        {
            var imageToCreate = new Image()
            {
                Url = "add test url",
                IsPreview = true,
            };
            var command = new CreateImageCommand()
            {
                Url = imageToCreate.Url,
                IsPreview = true,
            };
            var handler = new CreateImageCommandHandler(UnitOfWork);

            var createdImageId = await handler.Handle(command, CancellationToken.None);

            DbContext.Images.SingleOrDefault(x => x.ImageId == createdImageId
                                                  && x.Url == imageToCreate.Url
                                                  && x.IsPreview == imageToCreate.IsPreview).ShouldNotBeNull();
        }
    }
}
