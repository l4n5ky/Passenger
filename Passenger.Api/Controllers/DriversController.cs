using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;
using System;
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

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var driver = await _driverService.GetAsync(userId);
            if (driver == null)
            {
                return NotFound();
            }

            return Json(driver);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateDriver command)
        {
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
