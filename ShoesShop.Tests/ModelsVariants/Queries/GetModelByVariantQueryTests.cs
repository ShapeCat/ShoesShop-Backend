using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.ModelsVariants.Commands;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsVariants.Queries
{
    public class GetModelByVariantQueryTests : AbstractQueryTests
    {
        public GetModelByVariantQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnModel_WhenCorrect()
        {
            var query = new GetModelByVariantQuery()
            {
                ModelVariantId = TestData.UpdateModelVariantId,
            };
            var handler = new GetModelByVariantQueryHandler(UnitOfWork, Mapper);

            var model = await handler.Handle(query, CancellationToken.None);

            model.ShouldBeOfType<ModelVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelVariantNotExists()
        {
            var query = new GetModelByVariantQuery()
            {
                ModelVariantId = Guid.NewGuid(),
            };
            var handler = new GetModelByVariantQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
