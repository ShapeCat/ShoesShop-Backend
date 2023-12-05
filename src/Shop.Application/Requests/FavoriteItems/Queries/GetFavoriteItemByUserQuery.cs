using AutoMapper;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Extensions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.FavoriteItems.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.FavoriteItems.Queries
{
    public record GetFavoriteItemByUserQuery : IRequest<IEnumerable<FavoriteItemVm>>
    {
        public Guid UserId { get; set; }
    }

    public class GetFavoriteItemByUserQueryValidator : AbstractValidator<GetFavoriteItemByUserQuery>
    {
        public GetFavoriteItemByUserQueryValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }

    public class GetFavoriteItemByUserQueryHandler : AbstractQueryHandler<GetFavoriteItemByUserQuery, IEnumerable<FavoriteItemVm>>
    {
        public GetFavoriteItemByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async override Task<IEnumerable<FavoriteItemVm>> Handle(GetFavoriteItemByUserQuery request, CancellationToken cancellationToken)
        {
            var userRepository = UnitOfWork.GetRepositoryOf<User>();
            var favoriteItemRepository = UnitOfWork.GetRepositoryOf<FavoritesItem>();
            if (!await userRepository.IsExistsAsync(request.UserId, cancellationToken))
            {
                throw new NotFoundException(request.UserId.ToString(), typeof(User));
            }
            var items = await favoriteItemRepository.FindAllAsync(x => x.UserId == request.UserId, cancellationToken);
            return Mapper.Map<IEnumerable<FavoriteItemVm>>(items);
        }
    }
}
