using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Queries
{
    public class GetAllModelSizesQueryTests : AbstractQueryTests
    {
        public GetAllModelSizesQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async void Should_ReturnAllSizes()
        {
            // Arraange
            var query = new GetAllModelSizesQuery();
            var handler = new GetAllModelSizesQueryHandler(UnitOfWork, Mapper);

            // Act
            var allAdresses = await handler.Handle(query, CancellationToken.None);

            // Assert
            allAdresses.ShouldAllBe(x => x is ModelSizeVm);
            allAdresses.Count().ShouldBe(2);
        }
    }
}
