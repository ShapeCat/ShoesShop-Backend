using AutoMapper;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Persistence.Repository;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Queries
{
    public class GetShoesSizesByShoesQueryTests : AbstractQueryTest
    {
        public GetShoesSizesByShoesQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_GetShoesSizes_WhenShoesExists()
        {
            // Arrange
            var query = new GetShoesSizesByShoesQuery()
            {
                ShoesId = ShoesShopTestContext.FullShoes,
            };
            var handler = new GetShoesSizesByShoesQueryHandler(unitOfWork, mapper);

            // Act
            var shoesSizes = await handler.Handle(query, CancellationToken.None);

            // Assert
            shoesSizes.ShouldAllBe(x => x is ShoesSizeVm);
            shoesSizes.Count().ShouldBe(1);
        }

        [Fact]
        public async Task Should_ThrowException_WhenShoesNotExists()
        {
            // Arrange
            var query = new GetShoesSizesByShoesQuery()
            {
                ShoesId = Guid.NewGuid(),
            };
            var handler = new GetShoesSizesByShoesQueryHandler(unitOfWork, mapper);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
