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

    [Collection("QueryCollection")]
    public class GetAllShoesQueryTests
    {
        private readonly IShoesRepository shoesRepository;
        private readonly IMapper mapper;

        public GetAllShoesQueryTests(QueryFixture fixture)
        {
            shoesRepository = new ShoesRepository(fixture.DbContext);
            mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Should_GetAllShoes()
        {
            // Arrange
            var query = new GetAllShoesQuery();
            var handler = new GetAllShoesQueryHandler(shoesRepository, mapper);

            // Act
            var allShoes = await handler.Handle(query, CancellationToken.None);

            // Assert
            allShoes.ShouldAllBe(x => x is ShoesVm);
            allShoes.Count().ShouldBe(ShoesShopTextContext.ItemsCount);
        }
    }
}
