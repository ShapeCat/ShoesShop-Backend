//using ShoesShop.Application.Requests.Prices.OutputVMs;
//using ShoesShop.Application.Requests.Prices.Queries;
//using ShoesShop.Tests.Core;
//using Shouldly;
//using Xunit;

//namespace ShoesShop.Tests.Prices.Queries
//{
//    public class GetAllPricesQueryTests : AbstractQueryTests
//    {
//        public GetAllPricesQueryTests(QueryFixture fixture) : base(fixture) { }

//        [Fact]
//        public async void Should_ReturnAllPrices()
//        {
//            // Arraange
//            var query = new GetAllPricesQuery();
//            var handler = new GetAllPricesQueryHandler(UnitOfWork, Mapper);

//            // Act
//            var allPrices = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            allPrices.ShouldAllBe(x => x is PriceVm);
//            allPrices.Count().ShouldBe(2);
//        }
//    }
//}
