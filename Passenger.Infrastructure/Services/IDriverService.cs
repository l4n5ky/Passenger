using Passenger.Infrastructure.DTO;
using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverService
    {
        Task<DriverDto> GetAsync(Guid userId);
    }
}
