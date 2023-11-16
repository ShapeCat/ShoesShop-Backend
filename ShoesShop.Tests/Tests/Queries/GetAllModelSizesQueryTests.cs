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
            var allAddresses = await handler.Handle(query, CancellationToken.None);

            // Assert
            allAddresses.ShouldAllBe(x => x is ModelSizeVm);
            allAddresses.Count().ShouldBe(2);
        }
    }
}
