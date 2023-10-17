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
    public class GetAllDescriptionQueryTests
    {
        private readonly IDescriptionRepository descriptionRepository;
        private IMapper mapper;

        public GetAllDescriptionQueryTests(QueryFixture fixture)
        {
            descriptionRepository = new DescriptionRepository(fixture.DbContext);
            mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Should_GetAllDescriptions()
        {
            // Arraange
            var query = new GetAllDescriptionsQuery();
            var handler = new GetAllDescriptionsQueryHandler(descriptionRepository, mapper);

            // Act
            var allDescriptions = await handler.Handle(query, CancellationToken.None);

            // Assert
            allDescriptions.ShouldAllBe(x => x is DescriptionVm);
            allDescriptions.Count().ShouldBe(2);
        }
    }
}
