using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.Models.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Models.Queries
{
    public class GetAlModelsQueryTests : AbstractQueryTests
    {
        public GetAlModelsQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async void Should_ReturnAllModels()
        {
            // Arraange
            var query = new GetAllModelsQuery();
            var handler = new GetAllModelsQueryHandler(UnitOfWork, Mapper);

            // Act
            var allModels = await handler.Handle(query, CancellationToken.None);

            // Assert
            allModels.ShouldAllBe(x => x is ModelVm);
            allModels.Count().ShouldBe(2);
            //Additional test for images
            allModels.ShouldAllBe(x => x.Images is IEnumerable<ImageVm>);
        }
    }
}

