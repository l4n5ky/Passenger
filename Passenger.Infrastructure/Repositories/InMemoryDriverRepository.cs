using Passenger.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Passenger.Core.Domain;
using System.Threading.Tasks;
using System.Linq;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryDriverRepository : IDriverRepository
    {
        private static ISet<Driver> _drivers = new HashSet<Driver>();


        public async Task<Driver> GetAsync(Guid userId)
            => await Task.FromResult(_drivers.SingleOrDefault(x => x.UserId == userId));

        public async Task<IEnumerable<Driver>> GetAllAsync()
            => await Task.FromResult(_drivers);

        public async Task AddAsync(Driver driver)
        {
            _drivers.Add(driver);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Driver driver)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Driver driver)
        {
            _drivers.Remove(driver);
            await Task.CompletedTask;
        }
    }
}
