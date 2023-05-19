using EventOrganizator.API.Helpers;
using EventOrganizator.Application.Abstractions;
using EventOrganizator.Application.Abstractions.Services;
using EventOrganizator.Application.DTOs.Member;
using EventOrganizator.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizator.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IUserService _userService;

        public MemberController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Member")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateMemberDTO updateMemberDTO)
        {         
            Response response = await _userService.UpdateMemberAsync(updateMemberDTO);  
            return CustomHttpResponse.Result(response);
        }
    }
}
