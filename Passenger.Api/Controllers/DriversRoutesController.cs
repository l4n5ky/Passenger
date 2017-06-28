using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateDriverRoute command)
        {
            await DispatchAsync(command);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var command = new DeleteDriverRoute
            {
                Name = name
            };

            await DispatchAsync(command);

            return NoContent();
        }
    }
}
