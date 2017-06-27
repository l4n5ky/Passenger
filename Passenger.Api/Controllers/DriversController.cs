using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Api.Controllers
{
    public class DriversController : ApiControllerBase
    {
        private readonly IDriverService _driverService; 

        public DriversController(ICommandDispatcher commandDispatcher, 
            IDriverService driverService) : base(commandDispatcher)
        {
            _driverService = driverService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<DriverDto>> Get()
            => await _driverService.BrowseAsync();

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateDriver command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}
