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
        public async Task Should_ReturnPrice_WhenCorrect()
        {
            var query = new GetSaleQuery()
            {
                SaleId = TestData.UpdateSaleId,
            };
            var handler = new GetSaleQueryHandler(UnitOfWork, Mapper);

            var sale = await handler.Handle(query, CancellationToken.None);

            sale.ShouldBeOfType<SaleVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenPriceNotExists()
        {
            var query = new GetSaleQuery()
            {
                SaleId = Guid.NewGuid(),
            };
            var handler = new GetSaleQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
