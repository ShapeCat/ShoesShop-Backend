using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Extensions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Reviews.Commands
{
    public class CreateReviewCommand : IRequest<Guid>
    {
        public Guid ModelId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? PublishDate { get; set; }
        public byte Rating { get; set; }
        public string Comment { get; set; }
    }

    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.ModelId).NotEqual(Guid.Empty);
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
            RuleFor(x => x.Rating).InclusiveBetween((byte)1, (byte)5);
        }
    }

    public class CreateReviewCommandHandler : AbstractCommandHandler<CreateReviewCommand, Guid>
    {
        public CreateReviewCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewRepository = UnitOfWork.GetRepositoryOf<Review>();
            var modelRepository = UnitOfWork.GetRepositoryOf<Model>();
            var userRepository = UnitOfWork.GetRepositoryOf<User>();
            if (!await modelRepository.IsExistsAsync(request.ModelId, cancellationToken)) throw new NotFoundException(request.ModelId.ToString(), typeof(Model));
            if (!await userRepository.IsExistsAsync(request.UserId, cancellationToken)) throw new NotFoundException(request.UserId.ToString(), typeof(User));
            var review = new Review(request.ModelId, request.UserId, request.Rating, request.Comment, request.PublishDate);
            await reviewRepository.AddAsync(review, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return review.ReviewId;
        }
    }
}
