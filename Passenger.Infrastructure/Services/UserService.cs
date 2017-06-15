using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using System;
using Passenger.Infrastructure.DTO;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Passenger.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task RegisterAsync(string email, string username, string password, string role)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email {email} already exist.");
            }

            var salt = Guid.NewGuid().ToString("N");
            user = new User(Guid.NewGuid(), email, username, role, password, salt);
            await _userRepository.AddAsync(user);
        }
    }
}
