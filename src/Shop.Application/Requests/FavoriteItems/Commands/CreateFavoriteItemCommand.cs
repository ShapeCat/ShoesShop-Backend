using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Extensions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.FavoriteItems.Commands
{
    public class CreateFavoriteItemCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ModelVariantId { get; set; }
    }

    public class CreateFavoriteItemCommandValidator : AbstractValidator<CreateFavoriteItemCommand>
    {
        public CreateFavoriteItemCommandValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
            RuleFor(x => x.ModelVariantId).NotEqual(Guid.Empty);
        }
    }

    public class CreateFavoriteItemCommandHandler : AbstractCommandHandler<CreateFavoriteItemCommand, Guid>
    {
        public CreateFavoriteItemCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async override Task<Guid> Handle(CreateFavoriteItemCommand request, CancellationToken cancellationToken)
        {
            var userRepository = UnitOfWork.GetRepositoryOf<User>();
            var modelVariantRepository = UnitOfWork.GetRepositoryOf<ModelVariant>();
            var favoriteItemRepository = UnitOfWork.GetRepositoryOf<FavoritesItem>();
            if (!await userRepository.IsExistsAsync(request.UserId, cancellationToken))
            {
                throw new NotFoundException(request.UserId.ToString(), typeof(User));
            }
            else if (!await modelVariantRepository.IsExistsAsync(request.ModelVariantId, cancellationToken))
            {
                throw new NotFoundException(request.ModelVariantId.ToString(), typeof(ModelVariant));
            }
            if (await favoriteItemRepository.FindFirstAsync(x => x.ModelVariantId == request.ModelVariantId
                                                                 && request.UserId == x.UserId, cancellationToken) is not null)
            {
                throw new AlreadyExistsException(request.ModelVariantId.ToString(), typeof(FavoritesItem));
            }
            var favoriteItem = new FavoritesItem(Guid.NewGuid(), request.UserId, request.ModelVariantId);
            await favoriteItemRepository.AddAsync(favoriteItem, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return favoriteItem.FavoriteItemId;
        }
    }
}
