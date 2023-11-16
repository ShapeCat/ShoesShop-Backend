using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Queries
{
    public class GetAddressQueryTests : AbstractQueryTests
    {
        public GetAddressQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnAddress_WhenAddressExists()
        {
            // Arrange
            var query = new GetAddressQuery()
            {
                AddressId = TestData.UpdateAddressId,
            };
            var handler = new GetAddressQueryHandler(UnitOfWork, Mapper);

            // Act
            var address = await handler.Handle(query, CancellationToken.None);

            // Assert
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

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }

    }
}
