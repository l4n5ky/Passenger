using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverRouteService : IService
    {
        Task AddAsync(Guid userId, string name,
            double startLat, double startLong,
            double endLat, double endLong);
    }
}
