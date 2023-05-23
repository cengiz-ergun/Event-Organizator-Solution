using AutoMapper;
using EventOrganizator.Application.Abstractions;
using EventOrganizator.Application.Abstractions.Services;
using EventOrganizator.Application.DTOs.City;
using EventOrganizator.Application.UoW;
using EventOrganizator.Domain.Entities;
using EventOrganizator.Persistence.UoW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Persistence.Services
{
    public class CityService : ICityService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CityService(IMapper mapper,
                           IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response> AddCity(CreateCityDTO cityToAddDto)
        {
            City city = await _unitOfWork.CityRepository.Get(c => c.Name.ToLower() == cityToAddDto.Name.ToLower());
            Response response = new();
            if (city != null && city.IsDeleted == false)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.Conflict;
                response.Errors.Add($"There is already a city named {cityToAddDto.Name}.");
            }
            else if (city != null && city.IsDeleted == true)
            {
                city.IsDeleted = false;
                city.Name = cityToAddDto.Name;
                await _unitOfWork.CityRepository.Update(city);
                response.HttpStatusCode = System.Net.HttpStatusCode.Created;
                response.Data.Add(_mapper.Map<CityDTO>(city));
            }
            else
            {
                city = await _unitOfWork.CityRepository.Add(_mapper.Map<City>(cityToAddDto));
                response.HttpStatusCode = System.Net.HttpStatusCode.Created;
                response.Data.Add(_mapper.Map<CityDTO>(city));
            }
            return response;
        }

        public async Task<Response> DeleteCity(DeleteCityDTO deleteCityDTO)
        {
            City city = await _unitOfWork.CityRepository.Get(c => c.Id == deleteCityDTO.Id && c.IsDeleted == false);
            Response response = new();
            if (city == null)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Errors.Add($"There isn't a city id with {deleteCityDTO.Id}");
            }
            else
            {
                if (city.Events.Where(e => e.EventStatus == Domain.Enum.EventStatus.Pending || e.EventStatus == Domain.Enum.EventStatus.Active).Count() > 0)
                {
                    response.HttpStatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                    response.Errors.Add($"Sorry. This city has one or more event which is at Active or Pending status.");
                }
                else
                {
                    await _unitOfWork.CityRepository.Delete(_mapper.Map<City>(city));
                    response.HttpStatusCode = System.Net.HttpStatusCode.NoContent;
                }
            }
            return response;
        }

        public async Task<Response> GetCity(GetCityQueryDTO getCityQueryDTO)
        {
            Response response = new();
            var query = _unitOfWork.CityRepository.Table;
            var data = query.Skip(getCityQueryDTO.Page * getCityQueryDTO.Size).Take(getCityQueryDTO.Size);
            if (data.Any())
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                response.Data = await data.Where(city => city.IsDeleted == false)
                                            .Select(city => _mapper.Map<CityDTO>(city))
                                            .ToListAsync<object>();
            }
            else
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.NoContent;
            }
            return response;
        }

        public async Task<Response> GetCityById(GetCityByIdDTO getCityByIdDTO)
        {
            City city = await _unitOfWork.CityRepository.Get(c => c.Id == getCityByIdDTO.Id && c.IsDeleted == false);
            Response response = new();
            if (city == null)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
                response.Errors.Add($"There isn't a city id with {getCityByIdDTO.Id}");
            }
            else
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                response.Data.Add(_mapper.Map<CityDTO>(city));
            }
            return response;
        }
    }
}
