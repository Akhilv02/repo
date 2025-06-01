using ePizzaHub.Core.Contract;
using ePizzaHub.Repositories.Contract;
using ePizzaHub.Models.ApiModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ePizzaHub.Infrastructure.Models;

namespace ePizzaHub.Core.Concrete
{
    public class UserService : IUserService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRoleRepository roleRepository, IUserRepository userRepository, IMapper mapper) 
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateUserRequestAsync(CreateUserRequest createUserRequest)
        {
            // 1. Insert record in User table and UserRoles table
            // 2. Hash password sending my end user

            var rolesDetails = _roleRepository.GetAll().Where(x => x.Name == "User").FirstOrDefault();

            if (rolesDetails != null)
            {
                var user = _mapper.Map<User>(createUserRequest);

                user.Roles.Add(rolesDetails);

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                await _userRepository.AddAsync(user);

                int rowsInserted = await _userRepository.CommitAsync();

                return rowsInserted > 0;
            }

            return false;
        }
    }
}
