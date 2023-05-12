using AutoMapper;
using EventOrganizator.Application.Abstractions.Services;
using EventOrganizator.Application.DTOs.AppUser;
using EventOrganizator.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<SignupUserResponseDTO> SignupUserAsync(SignupUserDTO signupUserDTO)
        {
            AppUser appUser = _mapper.Map<AppUser>(signupUserDTO);
            var identityResult = await _userManager.CreateAsync(appUser, signupUserDTO.Password);
            return _mapper.Map<SignupUserResponseDTO>(identityResult);
        }
    }
}
