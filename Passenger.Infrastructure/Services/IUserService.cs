﻿using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(string email);

        Task<IEnumerable<UserDto>> BrowseAsync();

        Task RegisterAsync(Guid userId, string email, string username, string password, string role);

        Task LoginAsync(string email, string password);
    }
}
