using ShoesShop.Application.Requests.ModelsVariants.OutputVMs;
using ShoesShop.Application.Requests.ModelsVariants.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsVariants.Queries
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
            var allAddresses = await handler.Handle(query, CancellationToken.None);

            // Assert
            allAddresses.ShouldAllBe(x => x is ModelVariantVm);
            allAddresses.Count().ShouldBe(2);
        }
    }
}
