using ShoesShop.Application.Requests.Queries;
using ShoesShop.Application.Requests.Queries.OutputVMs;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Tests.Queries
{
    public class GetAllModelVariantsQueryTests : AbstractQueryTests
    {
        public GetAllModelVariantsQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async void Should_ReturnAllModelVariants()
        {
            // Arraange
            var query = new GetAllModelVariantQuery();
            var handler = new GetAllModelVariantsQueryHander(UnitOfWork, Mapper);

            // Act
            var allAdresses = await handler.Handle(query, CancellationToken.None);

            // Assert
            allAdresses.ShouldAllBe(x => x is ModelVariantVm);
            allAdresses.Count().ShouldBe(2);
        }
    }
}
