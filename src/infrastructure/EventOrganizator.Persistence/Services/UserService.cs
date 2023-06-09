﻿using AutoMapper;
using EventOrganizator.Application.Abstractions;
using EventOrganizator.Application.Abstractions.Services;
using EventOrganizator.Application.Abstractions.Token;
using EventOrganizator.Application.Constants;
using EventOrganizator.Application.DTOs;
using EventOrganizator.Application.DTOs.AppUser;
using EventOrganizator.Application.DTOs.Member;
using EventOrganizator.Application.Exceptions;
using EventOrganizator.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly ITokenHandler _tokenHandler;
        private readonly IWorkingContext _workingContext;

        public UserService(UserManager<AppUser> userManager, 
                           IMapper mapper, 
                           ILogger<UserService> logger, 
                           SignInManager<AppUser> signInManager, 
                           ITokenHandler tokenHandler,
                           IWorkingContext workingContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _tokenHandler = tokenHandler;
            _workingContext = workingContext;
        }
        public async Task<SignupUserResponseDTO> SignupUserAsync(SignupUserDTO signupUserDTO)
        {
            AppUser appUser = _mapper.Map<AppUser>(signupUserDTO);
            var identityResult = await _userManager.CreateAsync(appUser, signupUserDTO.Password);
            SignupUserResponseDTO signupUserResponseDTO = new SignupUserResponseDTO();
            if (!identityResult.Succeeded)
            {
                signupUserResponseDTO.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                foreach (var error in identityResult.Errors)
                {
                    signupUserResponseDTO.Errors.Add(error.Description);
                }
                _logger.LogInformation($"Can not create user. E-mail: {signupUserDTO.Email}");
            }
            else
            {
                var data = _mapper.Map<SignupUserResponseData>(appUser);
                signupUserResponseDTO.Data.Add(data);
                signupUserResponseDTO.HttpStatusCode = System.Net.HttpStatusCode.Created;

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
                _logger.LogInformation($"User with Id:{appUser.Id}, Name:{appUser.FirstName}, Role:{roles.FirstOrDefault()} created.");
            }
            return signupUserResponseDTO;
        }

        public async Task<LoginUserResponseDTO> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDTO.Email);
            LoginUserResponseDTO loginUserResponseDTO = new LoginUserResponseDTO();
            if (user == null)
            {
                loginUserResponseDTO.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                loginUserResponseDTO.Errors.Add($"There is not a registered user with e-mail {loginUserDTO.Email}.");
                //loginUserResponseDTO.Message = $"There is not a registered user with e-mail {loginUserDTO.Email}.";
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginUserDTO.Password, false);
                if (!result.Succeeded)
                {
                    loginUserResponseDTO.HttpStatusCode = System.Net.HttpStatusCode.Unauthorized;
                    loginUserResponseDTO.Errors.Add($"Password is not valid.");
                    //loginUserResponseDTO.Message = $"Password is not valid.";
                }
                else
                {
                    loginUserResponseDTO.HttpStatusCode = System.Net.HttpStatusCode.OK;
                    var roles = await _userManager.GetRolesAsync(user);
                    Token token = _tokenHandler.CreateAccessToken(7, user, roles.FirstOrDefault());
                    loginUserResponseDTO.Data.Add(token);
                }
            }
            return loginUserResponseDTO;
        }

        public async Task<Response> UpdateMemberAsync(UpdateMemberDTO updateMemberDTO)
        {
            Response response = new();

            var currentUserEmail = _workingContext.GetEmail();
            var user = await _userManager.FindByEmailAsync(currentUserEmail);

            if (user == null) // For safety
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Errors.Add("There is no user with this e-mail.");
                return response;
            }

            var isValidOldPassword = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, updateMemberDTO.OldPassword);
            if (isValidOldPassword == PasswordVerificationResult.Failed)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                response.Errors.Add("Old password is wrong.");
                return response;
            }

            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, passwordResetToken, updateMemberDTO.NewPassword);

            if (!resetPasswordResult.Succeeded)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                response.Errors = resetPasswordResult.Errors.Select(kvp => kvp.Description).ToList();
                return response;
            }

            response.HttpStatusCode = System.Net.HttpStatusCode.NoContent;
            return response;
        }
    }
}
