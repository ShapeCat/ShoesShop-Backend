//using AutoMapper;
//using ShoesShop.Application.Interfaces;
//using ShoesShop.Application.Requests.Queries;
//using ShoesShop.Application.Requests.Queries.OutputVMs;
//using ShoesShop.Persistence.Repository;
//using ShoesShop.Tests.Core;
//using Shouldly;
//using Xunit;

//namespace ShoesShop.Tests.Tests.Queries
//{
//    public class GetAllShoesQueryTests : AbstractQueryTest
//    {
//        public GetAllShoesQueryTests(QueryFixture fixture) : base(fixture) { }

//        [Fact]
//        public async Task Should_GetAllShoes()
//        {
//            // Arrange
//            var query = new GetAllShoesQuery();
//            var handler = new GetAllShoesQueryHandler(unitOfWork, mapper);

//            // Act
//            var allShoes = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            allShoes.ShouldAllBe(x => x is ShoesVm);
//            allShoes.Count().ShouldBe(ShoesShopTestContext.ItemsCount);
//        }
//    }
//}
