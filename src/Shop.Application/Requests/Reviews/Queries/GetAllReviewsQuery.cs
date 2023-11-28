using AutoMapper;
using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Reviews.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Reviews.Queries
{
    public record GetAllReviewsQuery : IRequest<IEnumerable<ReviewVm>> { }

    public class GetAllReviewsQueryHandler : AbstractQueryHandler<GetAllReviewsQuery, IEnumerable<ReviewVm>>
    {
        public GetAllReviewsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<IEnumerable<ReviewVm>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviewRepository = UnitOfWork.GetRepositoryOf<Review>();
            var allReviews = await reviewRepository.GetAllAsync(cancellationToken);
            return Mapper.Map<IEnumerable<ReviewVm>>(allReviews);
        }
    }
}
