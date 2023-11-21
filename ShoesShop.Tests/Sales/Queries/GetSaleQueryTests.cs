using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Sales.OutputVMs;
using ShoesShop.Application.Requests.Sales.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Sales.Queries
{
    public class GetSaleQueryTests : AbstractQueryTests
    {
        public GetSaleQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnPrice_WhenPriceExists()
        {
            // Arrange
            var query = new GetSaleQuery()
            {
                SaleId = TestData.UpdateSaleId,
            };
            var handler = new GetSaleQueryHandler(UnitOfWork, Mapper);

            // Act
            var sale = await handler.Handle(query, CancellationToken.None);

            // Assert
            sale.ShouldBeOfType<SaleVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenPriceNotExists()
        {
            // Arrange
            var query = new GetSaleQuery()
            {
                SaleId = Guid.NewGuid(),
            };
            var handler = new GetSaleQueryHandler(UnitOfWork, Mapper);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
