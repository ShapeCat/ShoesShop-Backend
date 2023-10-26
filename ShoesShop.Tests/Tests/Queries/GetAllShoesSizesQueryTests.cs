using AutoMapper;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Persistence.Repository;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Queries
{
    public class GetAllShoesSizesQueryTests : AbstractQueryTest
    {
        public GetAllShoesSizesQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_GetAllShoesSizes()
        {
            // Arrange
            var query = new GetAllShoesSizesQuery();
            var handler = new GetAllShoesSizesQueryHandler(unitOfWork, mapper);

            // Act
            var shoesSizes = await handler.Handle(query, CancellationToken.None);

            // Assert
            shoesSizes.ShouldAllBe(x => x is ShoesSizeVm);
            shoesSizes.Count().ShouldBe(2);
        }
    }
}
