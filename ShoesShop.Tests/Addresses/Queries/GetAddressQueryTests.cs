using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Addresses.OutputVMs;
using ShoesShop.Application.Requests.Addresses.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Addresses.Queries
{
    public class GetAddressQueryTests : AbstractQueryTests
    {
        public GetAddressQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnAddress_WhenCorrect()
        {
            var query = new GetAddressQuery()
            {
                AddressId = TestData.UpdateAddressId,
            };
            var handler = new GetAddressQueryHandler(UnitOfWork, Mapper);

            var address = await handler.Handle(query, CancellationToken.None);

            address.ShouldBeOfType<AddressVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenAddressNotExists()
        {
            // Arrange
            var query = new GetAddressQuery()
            {
                AddressId = Guid.NewGuid(),
            };
            var handler = new GetAddressQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
