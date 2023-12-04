using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Extensions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.FavoriteItems.Commands
{
    public record DeleteFavoriteItemCommand : IRequest<Unit>
    {
        public Guid FavoriteItemId { get; set; }
    }

    public class DeleteFavoriteItemCommandValidator : AbstractValidator<DeleteFavoriteItemCommand>
    {
        public DeleteFavoriteItemCommandValidator()
        {
            RuleFor(x => x.FavoriteItemId).NotEqual(Guid.Empty);
        }
    }

    public class DeleteFavoriteItemCommandHandler : AbstractCommandHandler<DeleteFavoriteItemCommand, Unit>
    {
        public DeleteFavoriteItemCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async override Task<Unit> Handle(DeleteFavoriteItemCommand request, CancellationToken cancellationToken)
        {
            var favoriteItemRepository = UnitOfWork.GetRepositoryOf<FavoritesItem>();
            if (!await favoriteItemRepository.IsExistsAsync(request.FavoriteItemId, cancellationToken))
            {
                throw new NotFoundException(request.FavoriteItemId.ToString(), typeof(FavoritesItem));
            }
            await favoriteItemRepository.RemoveAsync(request.FavoriteItemId, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
