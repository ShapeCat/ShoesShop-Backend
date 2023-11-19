using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.ModelsVariants.OutputVMs;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsVariants.Queries
{
    public class GetModelByVariantQueryTests : AbstractQueryTests
    {
        public GetModelByVariantQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnMode_WhenModelVariantExists()
        {
            // Arrange
            var query = new GetModelByVariantQuery()
            {
                ModelVariantId = TestData.UpdateModelVariantId,
            };
            var handler = new GetModelByVariantQueryHandler(UnitOfWork, Mapper);

            // Act
            var model = await handler.Handle(query, CancellationToken.None);

            // Assert
            model.ShouldBeOfType<ModelVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelVariantNotExists()
        {
            // Arrange
            var query = new GetModelByVariantQuery()
            {
                ModelVariantId = Guid.NewGuid(),
            };
            var handler = new GetModelByVariantQueryHandler(UnitOfWork, Mapper);
            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
