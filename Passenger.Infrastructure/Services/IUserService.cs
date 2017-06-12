using Passenger.Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(string email);

        Task<IEnumerable<UserDto>> BrowseAsync();

        Task RegisterAsync(string email, string username, string password);
    }
}
