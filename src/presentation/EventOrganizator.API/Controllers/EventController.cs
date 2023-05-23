using EventOrganizator.API.Helpers;
using EventOrganizator.Application.Abstractions;
using EventOrganizator.Application.Abstractions.Services;
using EventOrganizator.Application.DTOs.City;
using EventOrganizator.Application.DTOs.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizator.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [Authorize(Roles = "Administrator,Member")]
        [HttpGet]
        public async Task<IActionResult> GetEvent()
        {
            Response response = await _eventService.GetEvent();
            return CustomHttpResponse.Result(response);
        }

        [Authorize(Roles = "Administrator,Member")]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEventById([FromRoute] GetEventByIdDTO getEventByIdDTO)
        {
            Response response = await _eventService.GetEventById(getEventByIdDTO);
            return CustomHttpResponse.Result(response);
        }

        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] CreateEventDTO createEventDTO)
        {
            Response response = await _eventService.AddEvent(createEventDTO);
            return CustomHttpResponse.Result(response);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPatch("{Id}")]
        public async Task<IActionResult> PatchEventByAdmin([FromRoute] int Id, [FromBody] PatchEventByAdministratorDTO patchEventByAdministratorDTO)
        {
            Response response = await _eventService.PatchEvent(Id, patchEventByAdministratorDTO);
            return CustomHttpResponse.Result(response);
        }

        [Authorize(Roles = "Member")]
        [HttpPatch]
        public async Task<IActionResult> PatchEventByMember([FromBody] PatchEventByMemberDTO patchEventByMemberDTO)
        {
            Response response = await _eventService.PatchEvent(patchEventByMemberDTO);
            return CustomHttpResponse.Result(response);
        }

        [Authorize(Roles = "Member")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> CancelEvent([FromRoute] int Id)
        {
            Response response = await _eventService.CancelEvent(Id);
            return CustomHttpResponse.Result(response);
        }
    }
}
