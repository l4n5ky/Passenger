using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Exceptions;
using Passenger.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Driver> GetOrFailAsync(this IDriverRepository repository,
            Guid userId)
        {
            var driver = await repository.GetAsync(userId);
            if (driver == null)
            {
                throw new ServiceException(Exceptions.ErrorCodes.DriverNotFound,
                    $"Driver with user id : {userId} was not found.");
            }

            return driver;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository repository,
            Guid userId)
        {
            var user = await repository.GetAsync(userId);
            if (user == null)
            {
                throw new ServiceException(Exceptions.ErrorCodes.UserNotFound,
                    $"User with id : {userId} was not found.");
            }

            return user;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository repository,
            string email)
        {
            var user = await repository.GetAsync(email);
            if (user == null)
            {
                throw new ServiceException(Exceptions.ErrorCodes.UserNotFound,
                    $"User with email : {email} was not found.");
            }

            return user;
        }
    }
}
