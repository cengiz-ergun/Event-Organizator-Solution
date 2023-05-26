using AutoMapper;
using EventOrganizator.Application.Abstractions;
using EventOrganizator.Application.Abstractions.Services;
using EventOrganizator.Application.DTOs.City;
using EventOrganizator.Application.DTOs.Event;
using EventOrganizator.Application.Repositories.Category;
using EventOrganizator.Application.UoW;
using EventOrganizator.Domain.Entities;
using EventOrganizator.Domain.Entities.Identity;
using EventOrganizator.Domain.Enum;
using EventOrganizator.Persistence.Repositories.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Persistence.Services
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IWorkingContext _workingContext;
        readonly UserManager<AppUser> _userManager;

        public EventService(IMapper mapper,
                           IUnitOfWork unitOfWork,
                           ICategoryReadRepository categoryReadRepository,
                           IWorkingContext workingContext,
                           UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryReadRepository = categoryReadRepository;
            _workingContext = workingContext;
            _userManager = userManager; 
        }
        public async Task<Response> AddEvent(CreateEventDTO createEventDTO)
        {
            City city = await _unitOfWork.CityRepository.Get(c => c.Id == createEventDTO.CityId);
            Category category = await _categoryReadRepository.GetByIdAsync((int)createEventDTO.CategoryId);
            Response response = new();

            // Event name must be unique. 
            Event @event = await _unitOfWork.EventRepository.Get(e => e.Name.ToLower() == createEventDTO.Name.ToLower());
            if (@event != null)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.Conflict;
                response.Errors.Add($"There is already an event named {createEventDTO.Name}.");
                return response;
            }

            // Event date must be greater than tomorrow
            if (createEventDTO.Date < DateTime.Now.AddHours(23))
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                response.Errors.Add($"Date must be greater than tomorrow.");
                return response;
            }

            // Number of people must be greater than 3
            if (createEventDTO.NumberOfPeople < 4)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                response.Errors.Add($"Number of people must be greater than 3.");
                return response;
            }

            if (category == null)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                response.Errors.Add($"Invalid Category Id. There is not a category with Id={createEventDTO.CategoryId}");
                return response;
            }
            if (city == null)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                response.Errors.Add($"Invalid City Id. There is not a city with Id={createEventDTO.CityId}");
                return response;
            }

            var currentUserEmail = _workingContext.GetEmail();
            AppUser appUser = await _userManager.FindByEmailAsync(currentUserEmail);

            @event = _mapper.Map<Event>(createEventDTO);
            @event.EventStatus = Domain.Enum.EventStatus.Pending;
            @event.CreatedByAppUserId = appUser.Id;

            @event = await _unitOfWork.EventRepository.Add(@event);

            response.HttpStatusCode = System.Net.HttpStatusCode.Created;
            response.Data.Add(_mapper.Map<EventDTO>(@event));
            return response;
        }

        public async Task<Response> GetEvent()
        {
            Response response = new();

            var query = _unitOfWork.EventRepository.Table;

            response.HttpStatusCode = System.Net.HttpStatusCode.OK;
            response.Data = await query.Select(e => _mapper.Map<EventDTO>(e)).ToListAsync<object>();

            return response;    
        }

        public async Task<Response> GetEventById(GetEventByIdDTO getEventByIdDTO)
        {
            Response response = new();

            Event @event = await _unitOfWork.EventRepository.Get(e => e.Id == getEventByIdDTO.Id);

            if (@event == null)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Errors.Add($"There isn't an event id with {getEventByIdDTO.Id}");
            }
            else
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                response.Data.Add(_mapper.Map<EventDTO>(@event));
            }
            return response;
        }

        public async Task<Response> PatchEvent(int Id, PatchEventByAdministratorDTO patchEventByAdministratorDTO)
        {
            Response response = new();

            Event @event = await _unitOfWork.EventRepository.Get(e => e.Id == Id);

            if (@event == null)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Errors.Add($"There isn't an event id with {Id}");
                return response;
            }

            if (!typeof(EventStatus).IsEnumDefined(patchEventByAdministratorDTO.EventStatus))
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                response.Errors.Add($"Not valid event status.");
                return response;
            }

            @event.EventStatus = (EventStatus)patchEventByAdministratorDTO.EventStatus;
            @event = await _unitOfWork.EventRepository.Update(@event);
            response.HttpStatusCode = System.Net.HttpStatusCode.NoContent;
            return response;
        }

        public async Task<Response> PatchEvent(PatchEventByMemberDTO patchEventByMemberDTO)
        {
            Response response = new();

            Event @event = await _unitOfWork.EventRepository.Get(e => e.Id == patchEventByMemberDTO.Id);

            var currentUserEmail = _workingContext.GetEmail();
            AppUser appUser = await _userManager.FindByEmailAsync(currentUserEmail);
            if (appUser.Id != @event.CreatedByAppUserId)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.Unauthorized;
                response.Errors.Add($"You are not allowed to update this event.");
                return response;
            }

            bool isThereAnyChange = false;

            if (@event == null)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Errors.Add($"There isn't an event id with {patchEventByMemberDTO.Id}");
                return response;
            }

            if ((@event.Date - DateTime.UtcNow).TotalDays < 5)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                response.Errors.Add($"You can not update event due to there are 5 days to it.");
                return response;
            }

            if (patchEventByMemberDTO.Address != null)
            {
                @event.Address = patchEventByMemberDTO.Address;
                isThereAnyChange = true;
            }

            if(patchEventByMemberDTO.NumberOfPeople != null)
            {
                if(patchEventByMemberDTO.NumberOfPeople < 4)
                {
                    response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                    response.Errors.Add($"Number of people must be greater than 3.");
                    return response;
                }
                @event.NumberOfPeople = (int)patchEventByMemberDTO.NumberOfPeople;
                isThereAnyChange = true;
            }

            if (isThereAnyChange)
            {
                await _unitOfWork.EventRepository.Update(@event);
                response.HttpStatusCode = System.Net.HttpStatusCode.NoContent;
            }
            else{
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                response.Errors.Add($"There is nothing to change.");
            }

            return response;           
        }

        public async Task<Response> CancelEvent(int Id)
        {
            Response response = new();

            Event @event = await _unitOfWork.EventRepository.Get(e => e.Id == Id);

            if (@event == null)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Errors.Add($"There isn't an event id with {Id}");
                return response;
            }

            var currentUserEmail = _workingContext.GetEmail();
            AppUser appUser = await _userManager.FindByEmailAsync(currentUserEmail);
            if (appUser.Id != @event.CreatedByAppUserId)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.Unauthorized;
                response.Errors.Add($"You are not allowed to cancel this event.");
                return response;
            }

            if ((@event.Date - DateTime.UtcNow).TotalDays < 5)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                response.Errors.Add($"You can not cancel the event due to there are 5 days to it.");
                return response;
            }

            @event.EventStatus = EventStatus.Canceled;
            await _unitOfWork.EventRepository.Update(@event);
            response.HttpStatusCode = System.Net.HttpStatusCode.NoContent;
            return response;
        }

        public async Task<Response> HardDeleteEvent(int Id)
        {
            Response response = new();

            Event @event = await _unitOfWork.EventRepository.Get(e => e.Id == Id);

            if (@event == null)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Errors.Add($"There isn't an event id with {Id}");
                return response;
            }

            await _unitOfWork.EventRepository.HardDelete(@event);
            response.HttpStatusCode = System.Net.HttpStatusCode.NoContent;
            return response;
        }
    }
}
