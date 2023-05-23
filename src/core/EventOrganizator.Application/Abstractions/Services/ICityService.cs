using EventOrganizator.Application.DTOs.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Abstractions.Services
{
    public interface ICityService
    {
        Task<Response> AddCity(CreateCityDTO cityToAddDto);
        Task<Response> DeleteCity(DeleteCityDTO deleteCityDTO);
        Task<Response> GetCity(GetCityQueryDTO getCityQueryDTO);
        Task<Response> GetCityById(GetCityByIdDTO getCityByIdDTO);
    }
}
