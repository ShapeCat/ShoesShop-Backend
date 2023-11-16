using ShoesShop.Application.Requests.Adresses.Queries;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Addresses.Queries
{
    public class GetAllAddressesQueryTests : AbstractQueryTests
    {
        public GetAllAddressesQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnAllAddresses()
        {
            // Arraange
            var query = new GetAllAddressesQuery();
            var handler = new GetAllAddressesQueryHander(UnitOfWork, Mapper);

            // Act
            var allAddresses = await handler.Handle(query, CancellationToken.None);

            // Assert
            allAddresses.ShouldAllBe(x => x is AddressVm);
            allAddresses.Count().ShouldBe(2);
        }
    }
}
