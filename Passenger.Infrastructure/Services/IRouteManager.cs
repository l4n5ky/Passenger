using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IRouteManager : IService
    {
        Task<string> GetAddressAsync(double latitude, double longitude);
        double CalculateDistance(double startLat, double startLong, double endLat, double endLong);
    }
}
