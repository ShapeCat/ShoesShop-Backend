using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Authentication.Commands
{
    public record RegisterUserCommand : IRequest<Unit>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Login).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }

    public class RegisterUserCommandHandler : AbstractCommandHandler<RegisterUserCommand, Unit>
    {
        public RegisterUserCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userRepository = UnitOfWork.GetRepositoryOf<User>();
                var sameUsers = await userRepository.FindAllAsync(x => x.Login == request.Login, cancellationToken);
                if (sameUsers.Any()) throw new AlreadyExistsException(request.Login, typeof(User));
                var user = new User(request.Login, request.Password);
                await userRepository.AddAsync(user, cancellationToken);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (AlreadyExistsException) { throw; }
        }
    }
}
