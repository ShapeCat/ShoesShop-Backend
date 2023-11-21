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
            var modelToCreate = new Model()
            {
                Name = "create test name",
                Color = "create test color",
                Brand = "create test brand",
                SkuId = "create test SkuId",
                ReleaseDate = DateTime.Now,
            };
            var command = new CreateModelCommand()
            {
                Name = modelToCreate.Name,
                Color = modelToCreate.Color,
                Brand = modelToCreate.Brand,
                SkuId = modelToCreate.SkuId,
                ReleaseDate = modelToCreate.ReleaseDate,
            };
            var handler = new CreateModelCommandHandler(UnitOfWork);

            var createdModelId = await handler.Handle(command, CancellationToken.None);
            DbContext.Models.SingleOrDefault(x => x.ModelId == createdModelId
                                                 && x.Name == modelToCreate.Name
                                                 && x.Color == modelToCreate.Color
                                                 && x.Brand == modelToCreate.Brand
                                                 && x.SkuId == modelToCreate.SkuId
                                                 && x.ReleaseDate == modelToCreate.ReleaseDate).ShouldNotBeNull();
        }
    }
}
