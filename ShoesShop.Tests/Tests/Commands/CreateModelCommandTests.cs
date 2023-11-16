using ShoesShop.Application.Requests.Commands;
using ShoesShop.Entities;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Commands
{
    public class CreateModelCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_CreateModel_WhenNotExists()
        {
            var modelToCreate = new Model()
            {
                Name = "create test name",
                Color = "create test color",
                Brend = "create test brend",
                SkuId = "create test skuid",
                ReleaseDate = DateTime.Now,
            };
            var command = new CreateModelCommand()
            {
                Name = modelToCreate.Name,
                Color = modelToCreate.Color,
                Brend = modelToCreate.Brend,
                SkuId = modelToCreate.SkuId,
                ReleaseDate = modelToCreate.ReleaseDate,
            };
            var handler = new CreateModelCommandHandler(UnitOfWork);

            var createdModelId = await handler.Handle(command, CancellationToken.None);
            DbContext.Models.SingleOrDefault(x => x.ModelId == createdModelId
                                                 && x.Name == modelToCreate.Name
                                                 && x.Color == modelToCreate.Color
                                                 && x.Brend == modelToCreate.Brend
                                                 && x.SkuId == modelToCreate.SkuId
                                                 && x.ReleaseDate == modelToCreate.ReleaseDate).ShouldNotBeNull();
        }
    }
}
