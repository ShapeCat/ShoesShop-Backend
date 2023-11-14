using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Queries
{
    public class GetModelSizeQueryTests : AbstractQueryTests
    {
        public GetModelSizeQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async void Should_ReturnModelSize_WhenModelSizeExists()
        {
            var query = new GetModelSizeQuery()
            {
                ModelSizeId = TestData.UpdateModelSizeId,
            };
            var handler = new GetModelSizeQueryHandler(UnitOfWork, Mapper);

            // Act
            var modelSize = await handler.Handle(query, CancellationToken.None);

            // Assert
            modelSize.ShouldBeOfType<ModelSizeVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelSizeNotExists()
        {
            // Arrange
            var query = new GetModelSizeQuery()
            {
                ModelSizeId = Guid.NewGuid(),
            };
            var handler = new GetModelSizeQueryHandler(UnitOfWork, Mapper);
            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
