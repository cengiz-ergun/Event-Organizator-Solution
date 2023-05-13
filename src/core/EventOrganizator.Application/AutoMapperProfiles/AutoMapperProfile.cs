using AutoMapper;
using EventOrganizator.Application.DTOs.AppUser;
using EventOrganizator.Application.Features.Commands.AppUser;
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


        }
    }
}
