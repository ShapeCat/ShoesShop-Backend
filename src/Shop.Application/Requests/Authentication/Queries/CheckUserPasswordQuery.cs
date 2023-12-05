using AutoMapper;
using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Application.Requests.Authentication.OutputVMs;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Authentication.Queries
{
    public record CheckUserPasswordQuery : IRequest<AuthenticatedDataVm>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class CheckUserPasswordCommandValidator : AbstractValidator<CheckUserPasswordQuery>
    {
        public CheckUserPasswordCommandValidator()
        {
            RuleFor(x => x.Login).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }

    public class CheckUserPasswordCommandHandler : AbstractQueryHandler<CheckUserPasswordQuery, AuthenticatedDataVm>
    {
        public CheckUserPasswordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override async Task<AuthenticatedDataVm> Handle(CheckUserPasswordQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userRepository = UnitOfWork.GetRepositoryOf<User>();
                var user = (await userRepository.FindAllAsync(x => x.Login == request.Login, cancellationToken)).FirstOrDefault()
                           ?? throw new NotFoundException(request.Login, typeof(User));
                if (user.IsValidPassword(request.Password)) return Mapper.Map<AuthenticatedDataVm>(user);
                else throw new AuthenticationException(request.Login);
            }
            catch (NotFoundException) { throw; }
        }
    }
}
