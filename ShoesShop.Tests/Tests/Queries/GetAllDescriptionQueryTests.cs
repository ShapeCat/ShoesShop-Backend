using AutoMapper;
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
    public class GetAllDescriptionQueryTests : QueryTestAbstract
    {
        public GetAllDescriptionQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_GetAllDescriptions()
        {
            // Arraange
            var query = new GetAllDescriptionsQuery();
            var handler = new GetAllDescriptionsQueryHandler(unitOfWork, mapper);

            // Act
            var allDescriptions = await handler.Handle(query, CancellationToken.None);

            // Assert
            allDescriptions.ShouldAllBe(x => x is DescriptionVm);
            allDescriptions.Count().ShouldBe(2);
        }
    }
}
