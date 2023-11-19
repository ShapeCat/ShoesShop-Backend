using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Application.Requests.ModelsVariants.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsVariants.Queries
{
    public class GetModelSizeByVariantQueryTests : AbstractQueryTests
    {
        public GetModelSizeByVariantQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnModelSize_WhenModelVariantExists()
        {
            // Arrange
            var query = new GetModelSizeByVariantQuery()
            {
                ModelVariantId = TestData.UpdateModelVariantId,
            };
            var handler = new GetModelSizeByVariantQueryHandler(UnitOfWork, Mapper);

            // Act
            var modelSize = await handler.Handle(query, CancellationToken.None);

            // Assert
            modelSize.ShouldBeOfType<ModelSizeVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelVariantNotExists()
        {
            // Arrange
            var query = new GetModelSizeByVariantQuery()
            {
                ModelVariantId = Guid.NewGuid(),
            };
            var handler = new GetModelSizeByVariantQueryHandler(UnitOfWork, Mapper);

            // Act
            // Assert
            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
