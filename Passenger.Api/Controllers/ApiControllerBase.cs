using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    public class ApiControllerBase : Controller
    {
        protected readonly ICommandDispatcher CommandDispatcher;

        protected ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;
        }
    }
}
