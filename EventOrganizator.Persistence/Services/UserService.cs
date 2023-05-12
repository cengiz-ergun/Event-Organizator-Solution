using AutoMapper;
using EventOrganizator.Application.Abstractions.Services;
using EventOrganizator.Application.Constants;
using EventOrganizator.Application.DTOs.AppUser;
using EventOrganizator.Application.Exceptions;
using EventOrganizator.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public UserService(UserManager<AppUser> userManager, IMapper mapper, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<SignupUserResponseDTO> SignupUserAsync(SignupUserDTO signupUserDTO)
        {
            AppUser appUser = _mapper.Map<AppUser>(signupUserDTO);
            var identityResult = await _userManager.CreateAsync(appUser, signupUserDTO.Password);
            SignupUserResponseDTO signupUserResponseDTO = new SignupUserResponseDTO();
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    signupUserResponseDTO.Errors.Add(error.Description);
                }
                _logger.LogInformation($"Can not create user. E-mail: {signupUserDTO.Email}");
            }
            else
            {
                // if it is first user, assign it as admin
                var userCount = _userManager.Users.Count();
                if (userCount == 1)
                {
                    await _userManager.AddToRolesAsync(appUser, new string[] { Roles.Administrator });
                }
                else
                {
                    await _userManager.AddToRolesAsync(appUser, new string[] { Roles.Member });
                }
                var roles = await _userManager.GetRolesAsync(appUser);
                if (roles.Count != 1)
                {
                    throw new UserHasNotOneRoleException();
                }
                _logger.LogInformation($"User with Id:{appUser.Id}, Name:{appUser.FirstName}, Role:{roles.FirstOrDefault()} created.");
            }
            return signupUserResponseDTO;
        }
    }
}
