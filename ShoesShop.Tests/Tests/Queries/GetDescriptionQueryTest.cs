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
    public class GetDescriptionQueryTest
    {
        private readonly IDescriptionRepository descriptionRepository;
        private IMapper mapper;

        public GetDescriptionQueryTest(QueryFixture fixture)
        {
            descriptionRepository = new DescriptionRepository(fixture.DbContext);
            mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Should_GetDescription_IfExists()
        {
            // Arrange
            var query = new GetDescriptionQuery()
            {
                DescriptionId = ShoesShopTextContext.DescriptionToUpdate,
            };
            var handler = new GetDescriptionQueryHandler(descriptionRepository, mapper);

            // Act
            var description = await handler.Handle(query, CancellationToken.None);

            // Assert
            description.ShouldBeOfType<DescriptionVm>();
            description.ColorName.ShouldBe("TestColor1");
            description.ReleaseDate.ShouldBe(new DateTime(2024, 2, 6));
            description.SkuID.ShouldBe("TestSkuID_1");
        }

        [Fact]
        public async Task Should_ThrowException_WhenDescriptionNotExists()
        {
            // Arrange
            var query = new GetDescriptionQuery()
            {
                DescriptionId = Guid.NewGuid(),
            };
            var handler = new GetDescriptionQueryHandler(descriptionRepository, mapper);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
