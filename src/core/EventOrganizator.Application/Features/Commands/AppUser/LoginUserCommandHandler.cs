using AutoMapper;
using EventOrganizator.Application.Abstractions.Services;
using EventOrganizator.Application.DTOs.AppUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Features.Commands.AppUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IUserService _userService;
        private readonly IMapper _mapper;

        public LoginUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var loginUserDTO = _mapper.Map<LoginUserDTO>(request);
            LoginUserResponseDTO loginUserResponseDTO = await _userService.LoginUserAsync(loginUserDTO);
            LoginUserCommandResponse loginUserCommandResponse = _mapper.Map<LoginUserCommandResponse>(loginUserResponseDTO);

            return loginUserCommandResponse;
        }
    }
}
