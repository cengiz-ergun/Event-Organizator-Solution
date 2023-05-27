using EventOrganizator.Application.DTOs.City;
using EventOrganizator.Application.DTOs.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Abstractions.Services
{
    public interface IEventService
    {        
        Task<Response> AddEvent(CreateEventDTO createEventDTO);
        Task<Response> GetEvent(GetEventsByQueryDTO getEventsByQueryDTO);
        Task<Response> GetEventById(GetEventByIdDTO getEventByIdDTO);
        Task<Response> PatchEvent(int Id, PatchEventByAdministratorDTO patchEventByAdministratorDTO);
        Task<Response> PatchEvent(PatchEventByMemberDTO patchEventByMemberDTO);
        Task<Response> CancelEvent(int Id);
        Task<Response> HardDeleteEvent(int Id);
    }
}
