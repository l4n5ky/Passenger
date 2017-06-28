using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using System;
using System.Threading.Tasks;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        private readonly ICommandDispatcher CommandDispatcher;
        protected Guid UserId => User?.Identity?.IsAuthenticated == true ?
            Guid.Parse(User.Identity.Name) : Guid.Empty;

        protected ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command is IAuthenticatedCommand authenticatedCommand)
            {
                authenticatedCommand.UserId = UserId;
            }

            await CommandDispatcher.DispatchAsync(command);
        }
    }
}
