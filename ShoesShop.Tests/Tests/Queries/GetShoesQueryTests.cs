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
    public class GetShoesQueryTests
    {
        private readonly IShoesRepository shoesRepository;
        private readonly IMapper mapper;

        public GetShoesQueryTests(QueryFixture fixture)
        {
            shoesRepository = new ShoesRepository(fixture.DbContext);
            mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Should_GetShoes_WhenExists()
        {
            // Arrange
            var command = new GetShoesQuery()
            {
                ShoesId = ShoesShopTextContext.EmptyShoes,
            };
            var handler = new GetShoesQueryHandler(shoesRepository, mapper);

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
            var handler = new GetShoesQueryHandler(shoesRepository, mapper);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
