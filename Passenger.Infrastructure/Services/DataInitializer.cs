using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger(); 
        private readonly IUserService _userService;
        private readonly IDriverService _driverService;
        private readonly IDriverRouteService _driverRouteService;

        public DataInitializer(IUserService userService, IDriverService driverService,
            IDriverRouteService driverRouteService)
        {
            _userService = userService;
            _driverService = driverService;
            _driverRouteService = driverRouteService;
        }

        public async Task SeedAsync()
        {
            var users = await _userService.BrowseAsync();
            if(users.Any())
            {
                return;
            }

            Logger.Info("Initializing data...");
            for (int i = 1; i <= 10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";

                await _userService.RegisterAsync(userId, $"{username}@email.com", 
                    username, $"password{i}", "user");

                if (i % 3 == 1)
                {
                    await _driverService.CreateAsync(userId);
                    await _driverService.SetVehicleAsync(userId, "Audi", "RS8");
                    await _driverRouteService.AddAsync(userId, "Default", (i + 2) * 9, (i + 4) * 12, (i + 3) * 4, (i + 1) * 15);
                }
            }
            Logger.Info("Data was initialized successfully.");
        }
    }
}
