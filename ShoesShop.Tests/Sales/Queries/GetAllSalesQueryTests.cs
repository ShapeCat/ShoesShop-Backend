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
        public async void Should_ReturnAllSales_WhenCorrect()
        {
            var query = new GetAllSalesQuery();
            var handler = new GetAllSalesQueryHandler(UnitOfWork, Mapper);

            var allSales = await handler.Handle(query, CancellationToken.None);

            allSales.ShouldAllBe(x => x is SaleVm);
            allSales.Count().ShouldBe(2);
        }
    }
}
