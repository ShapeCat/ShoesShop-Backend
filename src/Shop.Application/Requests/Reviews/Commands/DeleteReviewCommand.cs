using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Reviews.Commands
{
    public record DeleteReviewCommand : IRequest<Unit>
    {
        public Guid ReviewId { get; set; }
    }

    public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
    {
        public DeleteReviewCommandValidator()
        {
            RuleFor(x => x.ReviewId).NotEqual(Guid.Empty);
        }
    }

    public class DeleteReviewCommandHandler : AbstractCommandHandler<DeleteReviewCommand, Unit>
    {
        public DeleteReviewCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewRepository = UnitOfWork.GetRepositoryOf<Review>();
            try
            {
                await reviewRepository.RemoveAsync(request.ReviewId, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (NotFoundException) { throw; }
            return Unit.Value;
        }
    }
}
