using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Extensions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ShopCartsItems.Commands
{
    public class DeleteShopCartItemCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid ModelVariantId { get; set; }
    }

    public class DeleteShopCartItemValidator : AbstractValidator<DeleteShopCartItemCommand>
    {
        public DeleteShopCartItemValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
            RuleFor(x => x.ModelVariantId).NotEqual(Guid.Empty);
        }
    }

    public class DeleteShopCartItemCommandHandler : AbstractCommandHandler<DeleteShopCartItemCommand, Unit>
    {
        public DeleteShopCartItemCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(DeleteShopCartItemCommand request, CancellationToken cancellationToken)
        {
            var shopCartItemRepository = UnitOfWork.GetRepositoryOf<ShopCartItem>();
            var userRepository = UnitOfWork.GetRepositoryOf<User>();
            if (!await userRepository.IsExistsAsync(request.UserId, cancellationToken))
            {
                throw new NotFoundException(request.UserId.ToString(), typeof(ShopCartItem));
            }
            var item = await shopCartItemRepository.FindFirstAsync(x => x.ModeVariantId == request.ModelVariantId
                                                                        && x.UserId == request.UserId, cancellationToken)
                        ?? throw new NotFoundException(request.ModelVariantId.ToString(), typeof(ShopCartItem));
            await shopCartItemRepository.RemoveAsync(item.ShopCartItemId, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
