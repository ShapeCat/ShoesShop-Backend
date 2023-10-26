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
    public class GetShoesSizeQueryTests : AbstractQueryTest
    {
        public GetShoesSizeQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_GetShoesSize_WhenShoesSizeExists()
        {
            // Arrange
            var query = new GetShoesSizeQuery()
            {
                ShoesSizeId = ShoesShopTestContext.ShoesSizeToUpdate,
            };
            var handler = new GetShoesSizeQueryHandler(unitOfWork, mapper);

            // Act
            var shoesSize = await handler.Handle(query, CancellationToken.None);

            // Assert
            shoesSize.ShouldBeOfType<ShoesSizeVm>();
            shoesSize.Size.ShouldBe(ShoesShopTestContext.ExistedSize);
            shoesSize.Price.ShouldBe(1000);
            shoesSize.ItemsLeft.ShouldBe(100);
        }

        [Fact]
        public async Task Should_ThrowException_WhenShoesSizeNotExists()
        {
            // Arrange
            var query = new GetShoesSizeQuery()
            {
                ShoesSizeId = Guid.NewGuid(),
            };
            var handler = new GetShoesSizeQueryHandler(unitOfWork, mapper);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
