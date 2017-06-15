﻿using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Handlers.Users
{
    public class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassword>
    {
        public async Task HandleAsync(ChangeUserPassword command)
        {
            // TODO: Change user password logic
            await Task.CompletedTask;
        }
    }
}
