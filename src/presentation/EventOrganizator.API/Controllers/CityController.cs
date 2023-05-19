using AutoMapper;
using EventOrganizator.API.Helpers;
using EventOrganizator.Application.Abstractions;
using EventOrganizator.Application.DTOs.Category;
using EventOrganizator.Application.DTOs.City;
using EventOrganizator.Application.UoW;
using EventOrganizator.Domain.Entities;
using EventOrganizator.Persistence.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq;

namespace EventOrganizator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityController(IUnitOfWork unitOfWork,
                              IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddCity([FromBody] CreateCityDTO cityToAddDto)
        {
            City city = await _unitOfWork.CityRepository.Get(c => c.Name == cityToAddDto.Name);
            Response response = new();
            if (city != null && city.IsDeleted == false)
            {
                response.HttpStatusCode = System.Net.HttpStatusCode.Conflict;
                response.Errors.Add($"There is already a city named {cityToAddDto.Name}.");
            }
            else if(city != null && city.IsDeleted == true)
            {
                city.IsDeleted = false;
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
            return CustomHttpResponse.Result(response);
        }


        [Authorize(Roles = "Administrator")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCity([FromRoute] DeleteCityDTO deleteCityDTO)
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
                await _unitOfWork.CityRepository.Delete(_mapper.Map<City>(city));
                response.HttpStatusCode = System.Net.HttpStatusCode.NoContent;
            }
            return CustomHttpResponse.Result(response);
        }

        [Authorize(Roles = "Administrator, Member")]
        [HttpGet]
        public async Task<IActionResult> GetCity([FromQuery] GetCityQueryDTO getCityQueryDTO)
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
            return CustomHttpResponse.Result(response);
        }

        [Authorize(Roles = "Administrator, Member")]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCityById([FromRoute] GetCityByIdDTO getCityByIdDTO)
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
            return CustomHttpResponse.Result(response);
        }
    }
}
