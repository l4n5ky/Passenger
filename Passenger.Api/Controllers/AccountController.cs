﻿using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using System.Threading.Tasks;

namespace Passenger.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        public AccountController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {

        }

        [HttpPut]
        [Route("password")]
        public async Task<IActionResult> Put([FromBody]ChangeUserPassword command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}
