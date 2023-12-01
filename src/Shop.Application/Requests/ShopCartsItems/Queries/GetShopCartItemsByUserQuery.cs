using AutoMapper;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Extensions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.ShopCartsItems.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ShopCartsItems.Queries
{
    public class GetShopCartItemsByUserQuery : IRequest<IEnumerable<ShopCartItemVm>>
    {
        public Guid UserId { get; set; }
    }

    public class GetShopCartItemsByUserQueryValidator : AbstractValidator<GetShopCartItemsByUserQuery>
    {
        public GetShopCartItemsByUserQueryValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }

    public class GetShopCartItemsByUserQueryHandler : AbstractQueryHandler<GetShopCartItemsByUserQuery, IEnumerable<ShopCartItemVm>>
    {
        public GetShopCartItemsByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<IEnumerable<ShopCartItemVm>> Handle(GetShopCartItemsByUserQuery request, CancellationToken cancellationToken)
        {
            var shopCartItemRepository = UnitOfWork.GetRepositoryOf<ShopCartItem>();
            var userRepository = UnitOfWork.GetRepositoryOf<User>();
            if (!await userRepository.IsExistsAsync(request.UserId, cancellationToken))
            {
                throw new NotFoundException(request.UserId.ToString(), typeof(User));
            }
            var userItems = await shopCartItemRepository.FindAllAsync(x => x.UserId == request.UserId, cancellationToken);
            return Mapper.Map<IEnumerable<ShopCartItemVm>>(userItems);
        }
    }
}
