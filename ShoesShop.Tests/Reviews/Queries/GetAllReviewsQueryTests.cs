using ShoesShop.Application.Requests.Reviews.OutputVMs;
using ShoesShop.Application.Requests.Reviews.Queries;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Reviews.Queries
{
    public class GetAllReviewsQueryTests : AbstractQueryTests
    {
        public GetAllReviewsQueryTests(QueryFixture fixture) : base(fixture) { }

        [Fact]
        public async void Should_ReturnAllReviews_WhenCorrect()
        {
            var query = new GetAllReviewsQuery();
            var handler = new GetAllReviewsQueryHandler(UnitOfWork, Mapper);

            var allReviews = await handler.Handle(query, CancellationToken.None);

            allReviews.ShouldAllBe(x => x is ReviewVm);
        }

    }
}
