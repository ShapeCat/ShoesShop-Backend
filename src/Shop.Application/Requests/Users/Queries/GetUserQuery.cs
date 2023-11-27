using AutoMapper;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Users.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Users.Queries
{
    public record GetUserQuery : IRequest<UserVm>
    {
        public Guid UserId { get; set; }
    }

    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }

    public class GetUserQueryHandler : AbstractQueryHandler<GetUserQuery, UserVm>
    {
        public GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<UserVm> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userRepository = UnitOfWork.GetRepositoryOf<User>();
                var user = await userRepository.GetAsync(request.UserId, cancellationToken);
                return Mapper.Map<UserVm>(user);
            }
            catch (NotFoundException) { throw; }
        }
    }
}
