using Passenger.Core.Domain;
using System;

namespace Passenger.Infrastructure.DTO
{
    public class DriverDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }
}
