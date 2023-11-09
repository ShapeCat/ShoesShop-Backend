using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShoesShop.Application.Interfaces;
using ShoesShop.Application.Requests.Base;
using ShoesShop.Entities;

namespace ShoesShop.Application.Requests.Commands
{
    public record CreateUserCommand : IRequest<Guid>
    {
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }

    public class CreateUserCommandHandler : AbstractCommandHandler, IRequestHandler<CreateUserCommand, Guid>
    {
        public CreateUserCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userRepository = unitOfWork.GetRepositoryOf<User>(true);
            var userToCreate = new User()
            {
                UserName = request.UserName,
                Login = request.Login,
                Phone = request.Phone,
            };

            await userRepository.AddAsync(userToCreate, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var createdUser = await userRepository.FindAllAsync(x => x.UserName == userToCreate.UserName
                                                                       && x.Login == userToCreate.Login
                                                                       && x.Phone == userToCreate.Phone, cancellationToken);
            return createdUser.First().Id;
        }
    }
}
