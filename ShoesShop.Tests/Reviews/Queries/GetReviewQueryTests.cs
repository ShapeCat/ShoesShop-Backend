using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Reviews.OutputVMs;
using ShoesShop.Application.Requests.Reviews.Queries;
using ShoesShop.Application.Requests.Sales.OutputVMs;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Reviews.Queries
{
    public class GetReviewQueryTests : AbstractQueryTests
    {
        public GetReviewQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Should_ReturnReview_WhenCorrect()
        {
            var query = new GetReviewQuery()
            {
                ReviewId = TestData.UpdateReviewId,
            };
            var handler = new GetReviewQueryHandler(UnitOfWork, Mapper);

            var review = await handler.Handle(query, CancellationToken.None);

            review.ShouldBeOfType<ReviewVm>();
        }

        [Fact]
        public async Task Should_ThrowException_WhenReviewNotExists()
        {
            var query = new GetReviewQuery()
            {
                ReviewId = Guid.NewGuid(),
            };
            var handler = new GetReviewQueryHandler(UnitOfWork, Mapper);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
