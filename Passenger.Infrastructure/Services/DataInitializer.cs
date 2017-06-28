using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IDriverService _driverService;
        private readonly IDriverRouteService _driverRouteService;
        private readonly ILogger<DataInitializer> _logger;

        public DataInitializer(IUserService userService, IDriverService driverService,
            IDriverRouteService driverRouteService, ILogger<DataInitializer> logger)
        {
            _userService = userService;
            _driverService = driverService;
            _driverRouteService = driverRouteService;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            _logger.LogTrace("Initializing data...");
            var tasks = new List<Task>();
            for (int i = 1; i <= 10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";

                tasks.Add(_userService.RegisterAsync(userId, $"{username}@email.com", username,
                    $"password{i}", "user"));

                if (i % 3 == 1)
                {
                    tasks.Add(_driverService.CreateAsync(userId));
                    tasks.Add(_driverService.SetVehicleAsync(userId, "Audi", "RS8"));
                    tasks.Add(_driverRouteService.AddAsync(userId, "Default", (i + 2) * 9, (i + 4) * 12, (i + 3) * 4, (i + 1) * 15));
                }
            }

            await Task.WhenAll(tasks);
            _logger.LogTrace("Data was initialized.");
        }
    }
}
