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
        public async void Should_ReturnAllModels_WhenCorrect()
        {
            var query = new GetAllModelsQuery();
            var handler = new GetAllModelsQueryHandler(UnitOfWork, Mapper);

            var allModels = await handler.Handle(query, CancellationToken.None);

            allModels.ShouldAllBe(x => x is ModelVm);
            allModels.Count().ShouldBe(2);
        }
    }
}

