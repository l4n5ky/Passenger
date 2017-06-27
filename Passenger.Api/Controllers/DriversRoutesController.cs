using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;
using System.Threading.Tasks;

namespace Passenger.Api.Controllers
{
    [Route("drivers/routes")]
    public class DriversRoutesController : ApiControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversRoutesController(IDriverService driverService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _driverService = driverService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateDriverRoute command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]CreateDriverRoute command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}
