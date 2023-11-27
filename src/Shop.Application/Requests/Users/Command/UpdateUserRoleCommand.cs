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

namespace ShoesShop.Application.Requests.Users.Command
{
    public record UpdateUserRoleCommand : IRequest<Unit>
    {
    public Guid UserId { get; set; }
    public Roles Role { get; set; }
    }

    public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleCommand>
    {
        public UpdateUserRoleValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }

    public class UpdateUserRoleCommandHandler : AbstractCommandHandler<UpdateUserRoleCommand, Unit>
    {
        public UpdateUserRoleCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)        {        }

        public override async Task<Unit> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            try{
                var userRepository = UnitOfWork.GetRepositoryOf<User>();
                var user =await userRepository.GetAsync(request.UserId, cancellationToken);
                user.Role = request.Role;
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch(NotFoundException){ throw; }
        }
    }
}
