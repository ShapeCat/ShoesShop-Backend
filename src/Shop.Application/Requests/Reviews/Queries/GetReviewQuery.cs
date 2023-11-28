using AutoMapper;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Reviews.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Reviews.Queries
{
    public record GetReviewQuery : IRequest<ReviewVm>
    {
        public Guid ReviewId { get; set; }
    }

    public class GetReviewQueryValidator : AbstractValidator<GetReviewQuery>
    {
        public GetReviewQueryValidator()
        {
            RuleFor(x => x.ReviewId).NotEqual(Guid.Empty);
        }
    }

    public class GetReviewQueryHandler : AbstractQueryHandler<GetReviewQuery, ReviewVm>
    {
        public GetReviewQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<ReviewVm> Handle(GetReviewQuery request, CancellationToken cancellationToken)
        {
            var reviewRepository = UnitOfWork.GetRepositoryOf<Review>();
            try
            {
                var review = await reviewRepository.GetAsync(request.ReviewId, cancellationToken);
                return Mapper.Map<ReviewVm>(review);
            }
            catch (NotFoundException) { throw; }
        }
    }
}
