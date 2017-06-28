using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Accounts;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace Passenger.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IMemoryCache _cache;

        public AccountController(ICommandDispatcher commandDispatcher, IMemoryCache cache) : base(commandDispatcher)
        {
            _cache = cache;
        }

        [HttpPut]
        [Route("password")]
        public async Task<IActionResult> Put([FromBody]ChangeUserPassword command)
        {
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
