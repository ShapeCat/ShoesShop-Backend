using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Application.Requests.ModelsSizes.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.ModelsSizes.Queries
{
    public class GetModelSizeQueryTests : AbstractQueryTests
    {
        public GetModelSizeQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async void Should_ReturnModelSize_WhenCorrect()
        {
            var query = new GetModelSizeQuery()
            {
                ModelSizeId = TestData.UpdateModelSizeId,
            };
            var handler = new GetModelSizeQueryHandler(UnitOfWork, Mapper);

            var modelSize = await handler.Handle(query, CancellationToken.None);

            modelSize.ShouldBeOfType<ModelSizeVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenModelSizeNotExists()
        {
            var query = new GetModelSizeQuery()
            {
                ModelSizeId = Guid.NewGuid(),
            };
            var handler = new GetModelSizeQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
