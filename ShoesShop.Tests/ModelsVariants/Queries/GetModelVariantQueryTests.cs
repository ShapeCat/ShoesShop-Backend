using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ModelsVariants.OutputVMs;
using ShoesShop.Application.Requests.ModelsVariants.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsVariants.Queries
{
    public class GetModelVariantQueryTests : AbstractQueryTests
    {
        public GetModelVariantQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnModeVariant_WhenCorrect()
        {
            var query = new GetModelVariantQuery()
            {
                ModelVariantId = TestData.UpdateModelVariantId,
            };
            var handler = new GetModelVariantQueryHandler(UnitOfWork, Mapper);

            var modelVariant = await handler.Handle(query, CancellationToken.None);

            modelVariant.ShouldBeOfType<ModelVariantVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelVariantNotExists()
        {
            var query = new GetModelVariantQuery()
            {
                ModelVariantId = Guid.NewGuid(),
            };
            var handler = new GetModelVariantQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
