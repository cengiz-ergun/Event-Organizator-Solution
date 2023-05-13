using EventOrganizator.API.Helpers;
using EventOrganizator.Application.Features.Commands.AppUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Signup(SignupUserCommandRequest signupUserCommandRequest)
        {
            SignupUserCommandResponse signupUserCommandResponse = await _mediator.Send(signupUserCommandRequest);
            return CustomHttpResponse.Result(signupUserCommandResponse);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse loginUserCommandResponse = await _mediator.Send(loginUserCommandRequest);
            return CustomHttpResponse.Result(loginUserCommandResponse);
        }
    }
}
