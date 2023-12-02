using AutoMapper;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.ShopCartsItems.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.ShopCartsItems.Queries
{
    public record GetShopCartItemQuery : IRequest<ShopCartItemVm>
    {
        public Guid ShopCartItemId { get; set; }
    }

    public class GetShopCartItemQueryValidator : AbstractValidator<GetShopCartItemQuery>
    {
        public GetShopCartItemQueryValidator()
        {
            RuleFor(x => x.ShopCartItemId).NotEqual(Guid.Empty);
        }
    }

    public class GetShopCartItemQueryHandler : AbstractQueryHandler<GetShopCartItemQuery, ShopCartItemVm>
    {
        public GetShopCartItemQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<ShopCartItemVm> Handle(GetShopCartItemQuery request, CancellationToken cancellationToken)
        {
            var shopCartItemRepository = UnitOfWork.GetRepositoryOf<ShopCartItem>();
            var item = await shopCartItemRepository.GetAsync(request.ShopCartItemId, cancellationToken);
            return Mapper.Map<ShopCartItemVm>(item);
        }
    }
}
