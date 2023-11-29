using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ShopCartsItems.Commands
{
    public class UpdateShopCartItemCommand : IRequest<Unit>
    {
        public Guid ShopCartItemId { get; set; }
        public int Amount { get; set; }
    }

    public class UpdateShopCartItemCommandValidator : AbstractValidator<UpdateShopCartItemCommand>{
        public UpdateShopCartItemCommandValidator()
        {
            RuleFor(x => x.ShopCartItemId).NotEqual(Guid.Empty);
            RuleFor(x=>x.Amount).GreaterThan(0).LessThan(int.MaxValue);
        }
    }

    public class UpdateShopCartItemCommandHandler : AbstractCommandHandler<UpdateShopCartItemCommand, Unit>
    {
        public UpdateShopCartItemCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)        {        }

        public override async Task<Unit> Handle(UpdateShopCartItemCommand request, CancellationToken cancellationToken)
        {
            var shopCartItemRepository = UnitOfWork.GetRepositoryOf<ShopCartItem>();
            var item = await shopCartItemRepository.GetAsync(request.ShopCartItemId, cancellationToken)
                       ?? throw new NotFoundException(request.ShopCartItemId.ToString(), typeof(ShopCartItem));
            item.Amount = request.Amount;
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
