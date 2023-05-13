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

namespace EventOrganizator.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<SignupUserCommandRequestValidator>();

            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
