using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Commands.Users;
using System.Collections.Generic;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET /users/email
        [HttpGet("{email}")]
        public async Task<UserDto> GetAsync(string email)
            => await _userService.GetAsync(email);

        [HttpGet("")]
        public async Task<IEnumerable<UserDto>> GetAllAsync()
            => await _userService.BrowseAsync();

        [HttpPost("")]
        public async Task Post([FromBody]CreateUser request)
            => await _userService.RegisterAsync(request.Email, request.Username, request.Password);
    }
}
