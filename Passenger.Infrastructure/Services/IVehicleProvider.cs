using Passenger.Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IVehicleProvider : IService
    {
        Task<IEnumerable<VehicleDto>> BrowseAsync();
        Task<VehicleDto> GetAsync(string brand, string name);
    }
}
