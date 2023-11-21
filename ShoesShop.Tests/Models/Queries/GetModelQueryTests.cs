using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.Models.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Models.Queries
{
    public class GetModelQueryTests : AbstractQueryTests
    {
        public GetModelQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async void Should_ReturnModel_WhenCorrect()
        {
            var query = new GetModelQuery()
            {
                ModelId = TestData.UpdateModelId,
            };
            var handler = new GetModelQueryHandler(UnitOfWork, Mapper);

            var model = await handler.Handle(query, CancellationToken.None);

            model.ShouldBeOfType<ModelVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelNotExists()
        {
            var query = new GetModelQuery()
            {
                ModelId = Guid.NewGuid(),
            };
            var handler = new GetModelQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
