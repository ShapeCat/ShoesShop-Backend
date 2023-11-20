using ShoesShop.Application.Requests.Sales.OutputVMs;
using ShoesShop.Application.Requests.Sales.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Sales.Queries
{
    public class GetAllSalesQueryTests : AbstractQueryTests
    {
        public GetAllSalesQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async void Should_ReturnAllSales()
        {
            // Arraange
            var query = new GetAllSalesQuery();
            var handler = new GetAllSalesQueryHandler(UnitOfWork, Mapper);

            // Act
            var allSales = await handler.Handle(query, CancellationToken.None);

            // Assert
            allSales.ShouldAllBe(x => x is SaleVm);
            allSales.Count().ShouldBe(2);
        }
    }
}
