using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using System.Threading.Tasks;

namespace Passenger.Api.Controllers
{
    public class DriversController : ApiControllerBase
    {
        public DriversController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateDriver command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}
