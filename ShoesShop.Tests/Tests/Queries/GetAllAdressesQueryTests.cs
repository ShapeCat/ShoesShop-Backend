using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Queries
{
    public class GetAllAdressesQueryTests : AbstractQueryTests
    {
        public GetAllAdressesQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnAllAdresses()
        {
            // Arraange
            var query = new GetAllAdressesQuery();
            var handler = new GetAllAdressesQueryHander(UnitOfWork, Mapper);

            // Act
            var allAdresses = await handler.Handle(query, CancellationToken.None);

            // Assert
            allAdresses.ShouldAllBe(x => x is AdressVm);
            allAdresses.Count().ShouldBe(2);
        }
    }
}
