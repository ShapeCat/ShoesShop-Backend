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
        public Guid ShopCartItemId { get; set; }
    }

    public class DeleteShopCartItemValidator : AbstractValidator<DeleteShopCartItemCommand>
    {
        public DeleteShopCartItemValidator()
        {
            RuleFor(x => x.ShopCartItemId).NotEqual(Guid.Empty);
        }
    }

    public class DeleteShopCartItemCommandHandler : AbstractCommandHandler<DeleteShopCartItemCommand, Unit>
    {
        public DeleteShopCartItemCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(DeleteShopCartItemCommand request, CancellationToken cancellationToken)
        {
            var shopCartItemRepository = UnitOfWork.GetRepositoryOf<ShopCartItem>();
            if (!await shopCartItemRepository.IsExistsAsync(request.ShopCartItemId, cancellationToken))
            {
                throw new NotFoundException(request.ShopCartItemId.ToString(), typeof(ShopCartItem));
            }
            await shopCartItemRepository.RemoveAsync(request.ShopCartItemId, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
