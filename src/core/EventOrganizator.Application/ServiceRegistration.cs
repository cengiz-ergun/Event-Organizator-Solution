using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using EventOrganizator.Application.AutoMapperProfiles;
using FluentValidation.AspNetCore;
using FluentValidation;
using EventOrganizator.Application.Features.Commands.AppUser;
using EventOrganizator.Application.Features.Commands.Category;
using EventOrganizator.Application.DTOs.City;
using EventOrganizator.Application.DTOs.Event;

namespace EventOrganizator.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<SignupUserCommandRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginUserCommandRequestValidator>();

            services.AddValidatorsFromAssemblyContaining<CategoryCreateCommandRequestValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateCityDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateEventDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<GetEventByIdDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<PatchEventByAdministratorDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<PatchEventByMemberDTOValidator>();

            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
