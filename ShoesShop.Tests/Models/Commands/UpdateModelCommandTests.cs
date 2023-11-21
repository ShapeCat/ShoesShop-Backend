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
            var modelToUpdate = new Model()
            {
                ModelId = TestData.UpdateModelId,
                Name = "update test name",
                Color = "update test color",
                Brand = "update test brand",
                SkuId = "update test SkuId",
                ReleaseDate = DateTime.Now,
            };
            var command = new UpdateModelCommand()
            {
                ModelId = modelToUpdate.ModelId,
                Name = modelToUpdate.Name,
                Color = modelToUpdate.Color,
                Brand = modelToUpdate.Brand,
                SkuId = modelToUpdate.SkuId,
                ReleaseDate = modelToUpdate.ReleaseDate,
            };
            var handler = new UpdateModelCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.Models.SingleOrDefault(x => x.ModelId == TestData.UpdateModelId
                                                 && x.Name == modelToUpdate.Name
                                                 && x.Color == modelToUpdate.Color
                                                 && x.Brand == modelToUpdate.Brand
                                                 && x.SkuId == modelToUpdate.SkuId
                                                 && x.ReleaseDate == modelToUpdate.ReleaseDate).ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_ThrowException_WhenImageNotExists()
        {
            var modelToUpdate = new Model()
            {
                Name = "update test name",
                Color = "update test color",
                Brand = "update test brand",
                SkuId = "update test SkuId",
                ReleaseDate = DateTime.Now,
            };
            var command = new UpdateModelCommand()
            {
                ModelId = Guid.NewGuid(),
                Name = modelToUpdate.Name,
                Color = modelToUpdate.Color,
                Brand = modelToUpdate.Brand,
                SkuId = modelToUpdate.SkuId,
                ReleaseDate = modelToUpdate.ReleaseDate,
            };
            var handler = new UpdateModelCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
