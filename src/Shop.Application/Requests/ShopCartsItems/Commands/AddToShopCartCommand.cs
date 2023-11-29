using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Extensions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ShopCartsItems.Commands
{
    public record AddToShopCartCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ModelVariantId { get; set; }
        public int Amount { get; set; }
    }

    public class AddToShopCartCommandValidator : AbstractValidator<AddToShopCartCommand>
    {
        public AddToShopCartCommandValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
            RuleFor(x => x.ModelVariantId).NotEqual(Guid.Empty);
        }
    }

    public class AddToShopCartCommandHandler : AbstractCommandHandler<AddToShopCartCommand, Guid>
    {
        public AddToShopCartCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Guid> Handle(AddToShopCartCommand request, CancellationToken cancellationToken)
        {
            var userRepository = UnitOfWork.GetRepositoryOf<User>();
            var shopCartItemsRepository = UnitOfWork.GetRepositoryOf<ShopCartItem>();
            var modelVariantRepository = UnitOfWork.GetRepositoryOf<ModelVariant>();
            if (!await userRepository.IsExistsAsync(request.UserId, cancellationToken))
            {
                throw new NotFoundException(request.UserId.ToString(), typeof(User));
            }
            if (!await modelVariantRepository.IsExistsAsync(request.ModelVariantId, cancellationToken))
            {
                throw new NotFoundException(request.ModelVariantId.ToString(), typeof(ModelVariant));
            }
            if (await shopCartItemsRepository.FindFirstAsync(x => x.UserId == request.UserId
                                                                  && x.ModeVariantId == request.ModelVariantId, cancellationToken) is not null)
            {
                throw new AlreadyExistsException(request.ModelVariantId.ToString(), typeof(ShopCartItem));
            }

            var item = new ShopCartItem(request.UserId, request.ModelVariantId, request.Amount);
            await shopCartItemsRepository.AddAsync(item, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return item.ShopCartItemId;
        }
    }
}
