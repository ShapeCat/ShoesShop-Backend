using AutoMapper;
using MediatR;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Users.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Users.Queries
{
    public record GetUsersByRoleQuery : IRequest<IEnumerable<UserVm>>
    {
        public Roles Role { get; set; }
    }

    public class GetUserByRoleQueryHandler : AbstractQueryHandler<GetUsersByRoleQuery, IEnumerable<UserVm>>
    {
        public GetUserByRoleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<IEnumerable<UserVm>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
        {
            var userRepository = UnitOfWork.GetRepositoryOf<User>();
            var users = await userRepository.FindAllAsync(x => x.Role == request.Role, cancellationToken);
            return Mapper.Map<IEnumerable<UserVm>>(users);
        }
    }
}
