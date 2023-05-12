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
    public class SignupUserCommandHandler : IRequestHandler<SignupUserCommandRequest, SignupUserCommandResponse>
    {
        readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SignupUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<SignupUserCommandResponse> Handle(SignupUserCommandRequest request, CancellationToken cancellationToken)
        {
            var signupUserDTO = _mapper.Map<SignupUserDTO>(request);
            var signupUserResponseDTO = await _userService.SignupUserAsync(signupUserDTO);
            var signupUserCommandResponse = _mapper.Map<SignupUserCommandResponse>(signupUserResponseDTO);
            return signupUserCommandResponse;
        }
    }
}
