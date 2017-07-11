using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class RouteManager : IRouteManager
    {
        private static readonly Random Random = new Random();

        public double CalculateDistance(double startLat, double startLong, double endLat, double endLong)
            => Random.Next(500, 10000);

        public Task<string> GetAddressAsync(double latitude, double longitude)
            => Task.FromResult($"Sample address: {Random.Next(1, 100)}.");
    }
}
