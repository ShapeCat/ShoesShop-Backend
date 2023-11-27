using FluentValidation;
using MediatR;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Requests.Abstraction;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Users.Command
{
    public record UpdateUserCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public Guid? AddressId { get; set; }
        public string? Phone { get; set; }
    }

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }

    public class UpdateUserCommandHandler : AbstractCommandHandler<UpdateUserCommand, Unit>
    {
        public UpdateUserCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async override Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userRepository = UnitOfWork.GetRepositoryOf<User>();
                var user = await userRepository.GetAsync(request.UserId, cancellationToken);
                user.UserName = request.UserName ?? user.UserName;
                user.Phone = request.Phone ?? user.Phone;
                if (request.AddressId is not null)
                {
                    var addressRepository = UnitOfWork.GetRepositoryOf<Address>();
                    var address = await addressRepository.GetAsync((Guid)request.AddressId, cancellationToken);
                    user.AddressId = address.AddressId;
                }
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (NotFoundException) { throw; }
        }
    }
}
