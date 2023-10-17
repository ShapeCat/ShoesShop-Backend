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
    public class GetShoesSizesByShoesQueryTests
    {
        private readonly IShoesSizeRepository shoesSizeRepository;
        private readonly IMapper mapper;

        public GetShoesSizesByShoesQueryTests(QueryFixture fixture)
        {
            shoesSizeRepository = new ShoesSizeRepository(fixture.DbContext);
            mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Should_GetShoesSizes_WhenExists()
        {
            // Arrange
            var query = new GetShoesSizesByShoesQuery()
            {
                ShoesId = ShoesShopTextContext.FullShoes,
            };
            var handler = new GetShoesSizesByShoesQueryHandler(shoesSizeRepository, mapper);

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
            var handler = new GetShoesSizesByShoesQueryHandler(shoesSizeRepository, mapper);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
