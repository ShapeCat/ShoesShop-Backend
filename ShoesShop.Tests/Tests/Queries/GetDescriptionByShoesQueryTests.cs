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
    public class GetDescriptionByShoesQueryTests
    {
        private readonly IDescriptionRepository descriptionRepository;
        private IMapper mapper;

        public GetDescriptionByShoesQueryTests(QueryFixture fixture)
        {
            descriptionRepository = new DescriptionRepository(fixture.DbContext);
            mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Should_GetDescription_IfShoesExists()
        {
            // Arrange
            var query = new GetDescriptionByShoesQuery()
            {
                ShoesId = ShoesShopTextContext.FullShoes,
            };
            var handler = new GetDescriptionByShoesQueryHandler(descriptionRepository, mapper);

            // Act
            var description = await handler.Handle(query, CancellationToken.None);

            // Assert
            description.ShouldBeOfType<DescriptionVm>();
            description.ColorName.ShouldBe("TestColor1");
            description.ReleaseDate.ShouldBe(new DateTime(2024, 2, 6));
            description.SkuID.ShouldBe("TestSkuID_1");
        }

        [Fact]
        public async Task Should_ThrowExeption_WhenShoesNotExists()
        {
            // Arrange
            var query = new GetDescriptionByShoesQuery()
            {
                ShoesId = Guid.NewGuid(),
            };
            var handler = new GetDescriptionByShoesQueryHandler(descriptionRepository, mapper);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
