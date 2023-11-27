using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoesShop.Application.Common.Exceptions;
using ShoesShop.Application.Requests.Users.Command;
using ShoesShop.Tests.Core;
using Shouldly;
using Xunit;

namespace ShoesShop.Tests.Users.Commands
{
    public class UpdateUserRoleCommandTests : AbstractCommandTests
    {
        [Fact]
        public async void Should_UpdateUserRole_WhenCorrect()
        {
            var command = new UpdateUserRoleCommand()
            {
                UserId = TestData.UpdateUserId,
                Role = Entities.Roles.Manager
            };
            var handler = new UpdateUserRoleCommandHandler(UnitOfWork);

            await handler.Handle(command, CancellationToken.None);

            DbContext.Users.SingleOrDefault(x => x.UserId == command.UserId
                                                 && x.Role == command.Role).ShouldNotBeNull();
        }

        [Fact]
        public async void Should_ThrowException_WhenUserNotExists()
        {
            var command = new UpdateUserRoleCommand()
            {
                UserId = Guid.NewGuid(),
                Role = Entities.Roles.Manager
            };
            var handler = new UpdateUserRoleCommandHandler(UnitOfWork);

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
