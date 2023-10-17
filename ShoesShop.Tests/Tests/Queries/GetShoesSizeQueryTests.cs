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
    [Collection("QueryCollection")]
    public class GetShoesSizeQueryTests
    {
        private readonly IShoesSizeRepository shoesSizeRepository;
        private readonly IMapper mapper;

        public GetShoesSizeQueryTests(QueryFixture fixture)
        {
            shoesSizeRepository = new ShoesSizeRepository(fixture.DbContext);
            mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Should_GetShoesSize_WhenExists()
        {
            // Arrange
            var query = new GetShoesSizeQuery()
            {
                ShoesSizeId = ShoesShopTextContext.ShoesSizeToUpdate,
            };
            var handler = new GetShoesSizeQueryHandler(shoesSizeRepository, mapper);

            // Act
            var shoesSize = await handler.Handle(query, CancellationToken.None);

            // Assert
            shoesSize.ShouldBeOfType<ShoesSizeVm>();
            shoesSize.Size.ShouldBe(ShoesShopTextContext.ExistedSize);
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
            var handler = new GetShoesSizeQueryHandler(shoesSizeRepository, mapper);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
