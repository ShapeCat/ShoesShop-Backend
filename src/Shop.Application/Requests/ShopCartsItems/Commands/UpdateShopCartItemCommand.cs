using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Extensions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ShopCartsItems.Commands
{
    public class UpdateShopCartItemCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid ModelVariantId { get; set; }
        public int Amount { get; set; }
    }

    public class UpdateShopCartItemCommandValidator : AbstractValidator<UpdateShopCartItemCommand>{
        public UpdateShopCartItemCommandValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
            RuleFor(x => x.ModelVariantId).NotEqual(Guid.Empty);
            RuleFor(x=>x.Amount).GreaterThan(0).LessThan(int.MaxValue);
        }
    }

    public class UpdateShopCartItemCommandHandler : AbstractCommandHandler<UpdateShopCartItemCommand, Unit>
    {
        public UpdateShopCartItemCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)        {        }

        public override async Task<Unit> Handle(UpdateShopCartItemCommand request, CancellationToken cancellationToken)
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
            item.Amount = request.Amount;
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
