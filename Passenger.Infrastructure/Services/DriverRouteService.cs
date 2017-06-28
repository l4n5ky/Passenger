using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;
        
        public DriverRouteService(IUserRepository userRepository,
            IDriverRepository driverRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(Guid userId, string name, double startLat, double startLong, double endLat, double endLong)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with id {userId} doesn't exists.");
            }

            var start = Node.Create("Start address", startLat, startLong);
            var end = Node.Create("End address", endLat, endLong);
            driver.AddRoute(name, start, end);
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
