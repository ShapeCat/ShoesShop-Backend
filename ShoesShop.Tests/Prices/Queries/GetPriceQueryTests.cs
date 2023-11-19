//using ShoesShop.Application.Common.Exceptions;
//using ShoesShop.Application.Requests.Prices.OutputVMs;
//using ShoesShop.Application.Requests.Prices.Queries;
//using ShoesShop.Tests.Core;
//using Shouldly;
//using Xunit;

//namespace ShoesShop.Tests.Prices.Queries
//{
//    public class GetPriceQueryTests : AbstractQueryTests
//    {
//        public GetPriceQueryTests(QueryFixture fixture) : base(fixture) { }

//        [Fact]
//        public async Task Should_ReturnPrice_WhenPriceExists()
//        {
//            // Arrange
//            var query = new GetPriceQuery()
//            {
//                PriceId = TestData.UpdatePriceId,
//            };
//            var handler = new GetPriceQueryHandler(UnitOfWork, Mapper);

//            // Act
//            var price = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            price.ShouldBeOfType<PriceVm>();
//        }

//        [Fact]
//        public async Task Should_ThrowException_WhenPriceNotExists()
//        {
//            // Arrange
//            var query = new GetPriceQuery()
//            {
//                PriceId = Guid.NewGuid(),
//            };
//            var handler = new GetPriceQueryHandler(UnitOfWork, Mapper);

//            // Act
//            // Assert
//            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
//        }
//    }
//}
