using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Handlers.Drivers
{
    public class UpdateDriverHandler : ICommandHandler<UpdateDriver>
    {
        private readonly IDriverService _driverService;

        public UpdateDriverHandler(IDriverService driverService)
        {
            _driverService = driverService;
        }

        public async Task HandleAsync(UpdateDriver command)
        {
            var vehicle = command.Vehicle;
            await _driverService.SetVehicleAsync(command.UserId, vehicle.Brand, vehicle.Name);
        }
    }
}
