using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Extensions;
using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IRouteManager _routeManager;
        private readonly IMapper _mapper;
        
        public DriverRouteService(IUserRepository userRepository,
            IDriverRepository driverRepository, IRouteManager routeManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _driverRepository = driverRepository;
            _routeManager = routeManager;
            _mapper = mapper;
        }

        public async Task AddAsync(Guid userId, string name, double startLat, double startLong, double endLat, double endLong)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
            var startAddress = await _routeManager.GetAddressAsync(startLat, startLong);
            var endAddress = await _routeManager.GetAddressAsync(endLat, endLong);

            var startNode = Node.Create("Start address", startLat, startLong);
            var endNode = Node.Create("End address", endLat, endLong);
            var distance = _routeManager.CalculateDistance(startLat, startLong, endLat, endLong);

            driver.AddRoute(name, startNode, endNode, distance);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetAsync(userId);
            driver.DeleteRoute(name);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}
