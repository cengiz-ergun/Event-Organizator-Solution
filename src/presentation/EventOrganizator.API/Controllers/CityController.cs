using AutoMapper;
using EventOrganizator.API.Helpers;
using EventOrganizator.Application.Abstractions;
using EventOrganizator.Application.Abstractions.Services;
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
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddCity([FromBody] CreateCityDTO cityToAddDto)
        {
            Response response = await _cityService.AddCity(cityToAddDto);
            return CustomHttpResponse.Result(response);
        }


        [Authorize(Roles = "Administrator")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCity([FromRoute] DeleteCityDTO deleteCityDTO)
        {
            Response response = await _cityService.DeleteCity(deleteCityDTO);
            return CustomHttpResponse.Result(response);
        }

        [Authorize(Roles = "Administrator, Member")]
        [HttpGet]
        public async Task<IActionResult> GetCity([FromQuery] GetCityQueryDTO getCityQueryDTO)
        {
            Response response = await _cityService.GetCity(getCityQueryDTO);
            return CustomHttpResponse.Result(response);
        }

        [Authorize(Roles = "Administrator, Member")]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCityById([FromRoute] GetCityByIdDTO getCityByIdDTO)
        {
            Response response = await _cityService.GetCityById(getCityByIdDTO);
            return CustomHttpResponse.Result(response);
        }
    }
}
