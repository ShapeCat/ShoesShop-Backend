using ShoesShop.Application.Requests.Models.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Models.Commands
{
    public class CreateModelCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_CreateModel_WhenCorrect()
        {
            var command = new CreateModelCommand()
            {
                Name = "create test name",
                Color = "create test color",
                Brand = "create test brand",
                SkuId = "create test SkuId",
                ReleaseDate = DateTime.Now,
            };
            var handler = new CreateModelCommandHandler(UnitOfWork);

            var createdModelId = await handler.Handle(command, CancellationToken.None);
            DbContext.Models.SingleOrDefault(x => x.ModelId == createdModelId
                                                 && x.Name == command.Name
                                                 && x.Color == command.Color
                                                 && x.Brand == command.Brand
                                                 && x.SkuId == command.SkuId
                                                 && x.ReleaseDate == command.ReleaseDate).ShouldNotBeNull();
        }
    }
}
