using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Models.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Models.Commands
{
    public class UpdateModelCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_UpdateModel_WhenCorrect()
        {
            var command = new UpdateModelCommand()
            {
                ModelId = TestData.UpdateModelId,
                Name = "update test name",
                Color = "update test color",
                Brand = "update test brand",
                SkuId = "update test SkuId",
                ReleaseDate = DateTime.Now,
            };
            var handler = new UpdateModelCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.Models.SingleOrDefault(x => x.ModelId == TestData.UpdateModelId
                                                 && x.Name == command.Name
                                                 && x.Color == command.Color
                                                 && x.Brand == command.Brand
                                                 && x.SkuId == command.SkuId
                                                 && x.ReleaseDate == command.ReleaseDate).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenImageNotExists()
        {
            var command = new UpdateModelCommand()
            {
                Name = "update test name",
                Color = "update test color",
                Brand = "update test brand",
                SkuId = "update test SkuId",
                ReleaseDate = DateTime.Now,
            };
            var handler = new UpdateModelCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
