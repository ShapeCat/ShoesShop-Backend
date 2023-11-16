using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Queries
{
    public class GetModelQueryTests : AbstractQueryTests
    {
        public GetModelQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async void Should_ReturnModel_WhenMOdelExists()
        {
            // Arrange
            var query = new GetModelQuery()
            {
                ModelId = TestData.UpdateModelId,
            };
            var handler = new GetModelQueryHandler(UnitOfWork, Mapper);

            // Act
            var address = await handler.Handle(query, CancellationToken.None);

            // Assert
            address.ShouldBeOfType<ModelVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelNotExists()
        {
            // Arrange
            var query = new GetModelQuery()
            {
                ModelId = Guid.NewGuid(),
            };
            var handler = new GetModelQueryHandler(UnitOfWork, Mapper);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
