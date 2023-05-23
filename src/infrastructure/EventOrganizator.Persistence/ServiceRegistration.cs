using EventOrganizator.Application.Abstractions.Services;
using EventOrganizator.Domain.Entities.Identity;
using EventOrganizator.Persistence.Contexts;
using EventOrganizator.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventOrganizator.Persistence.Configuration;
using EventOrganizator.Application.Abstractions;
using Microsoft.Extensions.Options;
using EventOrganizator.Application.Repositories.Category;
using EventOrganizator.Persistence.Repositories.Category;
using EventOrganizator.Application.UoW;
using EventOrganizator.Persistence.UoW;

namespace EventOrganizator.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<EventOrganizatorDbContext>(options => options.UseSqlServer(DbConfiguration.ConnectionString));

            // AddIdentity extension method is accessed via Microsoft.AspNetCore.Identity.UI
            var builder = services.AddIdentity<AppUser, AppRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
                o.User.AllowedUserNameCharacters = null;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(AppRole), builder.Services);
            builder.AddEntityFrameworkStores<EventOrganizatorDbContext>()
                   .AddDefaultTokenProviders();

            services.AddScoped<IUserService, UserService>();            
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IEventService, EventService>();

            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
