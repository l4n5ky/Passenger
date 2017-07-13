using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Repositories
{
    public class DriverRepository : IDriverRepository, IMongoRepository
    {
        private readonly IMongoDatabase _database;
        
        public DriverRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Driver> GetAsync(Guid userId)
            => await Drivers.AsQueryable().FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task<IEnumerable<Driver>> GetAllAsync()
            => await Drivers.AsQueryable().ToListAsync();

        public async Task AddAsync(Driver driver)
            => await Drivers.InsertOneAsync(driver);

        public async Task DeleteAsync(Driver driver)
            => await Drivers.DeleteOneAsync(x => x.UserId == driver.UserId);

        public async Task UpdateAsync(Driver driver)
            => await Drivers.ReplaceOneAsync(x => x.UserId == driver.UserId, driver);

        private IMongoCollection<Driver> Drivers => _database.GetCollection<Driver>("Drivers");
    }
}
