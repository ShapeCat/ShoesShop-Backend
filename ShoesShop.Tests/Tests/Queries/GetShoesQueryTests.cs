using AutoMapper;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Persistence;
using ShoesShop.Persistence.Repository;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Queries
{
    public class GetShoesQueryTests : AbstractQueryTest
    {
        public GetShoesQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_GetShoes_WhenShoesExists()
        {
            // Arrange
            var command = new GetShoesQuery()
            {
                ShoesId = ShoesShopTestContext.EmptyShoes,
            };
            var handler = new GetShoesQueryHandler(unitOfWork, mapper);

            // Act
            var shoes = await handler.Handle(command, CancellationToken.None);

            // Assert
            shoes.ShouldBeOfType<ShoesVm>();
            shoes.Name.ShouldBe("Empty Shoes");
        }

        [Fact]
        public async Task Should_ThrowException_WhenShoesNotExists()
        {
            // Arrange
            var command = new GetShoesQuery()
            {
                ShoesId = Guid.NewGuid(),
            };
            var handler = new GetShoesQueryHandler(unitOfWork, mapper);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
