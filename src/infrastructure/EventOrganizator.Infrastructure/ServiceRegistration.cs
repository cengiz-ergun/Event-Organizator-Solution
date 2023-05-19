using EventOrganizator.Application.Abstractions.Services;
using EventOrganizator.Application.Abstractions.Token;
using EventOrganizator.Infrastructure.Services;
using EventOrganizator.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IWorkingContext, WorkingContext>();
        }
    }
}
