using AutoMapper;
using EventOrganizator.Application.Abstractions;
using EventOrganizator.Application.DTOs.AppUser;
using EventOrganizator.Application.DTOs.Category;
using EventOrganizator.Application.DTOs.City;
using EventOrganizator.Application.Features.Commands.AppUser;
using EventOrganizator.Application.Features.Commands.Category;
using EventOrganizator.Application.Features.Queries.Category;
using EventOrganizator.Domain.Entities;
using EventOrganizator.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SignupUserCommandRequest, SignupUserDTO>().ReverseMap();
            CreateMap<SignupUserDTO, AppUser>().ReverseMap();
            CreateMap<AppUser, SignupUserResponseData>().ReverseMap();
            CreateMap<IdentityResult, SignupUserResponseDTO>().ReverseMap();
            CreateMap<SignupUserResponseDTO, SignupUserCommandResponse>().ReverseMap();

            CreateMap<LoginUserCommandRequest, LoginUserDTO>().ReverseMap();
            CreateMap<LoginUserResponseDTO, LoginUserCommandResponse>().ReverseMap();

            CreateMap<CategoryCreateCommandRequest, CreateCategory>().ReverseMap();
            CreateMap<Category, CreateCategory>().ReverseMap();
            CreateMap<CategoryCreateCommandResponse, Response>().ReverseMap();
            CreateMap<Category, SingleCategory>().ReverseMap();

            CreateMap<Response, GetAllCategoriesQueryResponse>().ReverseMap();
            CreateMap<Response, GetCategoryByIdQueryResponse>().ReverseMap();
            CreateMap<Response, DeleteCategoryCommandResponse>().ReverseMap();


            CreateMap<City, CreateCityDTO>().ReverseMap();
            CreateMap<City, CityDTO>().ReverseMap();

        }
    }
}
