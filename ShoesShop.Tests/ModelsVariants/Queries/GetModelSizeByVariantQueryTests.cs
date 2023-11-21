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
        public async Task Should_ReturnModelSize_WhenCorrect()
        {
            var query = new GetModelSizeByVariantQuery()
            {
                ModelVariantId = TestData.UpdateModelVariantId,
            };
            var handler = new GetModelSizeByVariantQueryHandler(UnitOfWork, Mapper);

            var modelSize = await handler.Handle(query, CancellationToken.None);

            modelSize.ShouldBeOfType<ModelSizeVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelVariantNotExists()
        {
            var query = new GetModelSizeByVariantQuery()
            {
                ModelVariantId = Guid.NewGuid(),
            };
            var handler = new GetModelSizeByVariantQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
