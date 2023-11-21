using ShoesShop.Application.Requests.Addresses.OutputVMs;
using ShoesShop.Application.Requests.Addresses.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Addresses.Queries
{
    public class GetAllAddressesQueryTests : AbstractQueryTests
    {
        public GetAllAddressesQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnAllAddresses_WhenCorrect()
        {
            var query = new GetAllAddressesQuery();
            var handler = new GetAllAddressesQueryHander(UnitOfWork, Mapper);

            var allAddresses = await handler.Handle(query, CancellationToken.None);

            allAddresses.ShouldAllBe(x => x is AddressVm);
            allAddresses.Count().ShouldBe(2);
        }
    }
}
