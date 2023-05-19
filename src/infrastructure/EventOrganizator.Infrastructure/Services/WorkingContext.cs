using EventOrganizator.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Infrastructure.Services
{
    public class WorkingContext : IWorkingContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkingContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetEmail()
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Any())
            {
                return _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
                                                                   .FirstOrDefault().Value;
            }
            else
            {
                return null;
            }
        }

        public string GetFirstName()
        {
            throw new NotImplementedException();
        }

        public string GetLastName()
        {
            throw new NotImplementedException();
        }

        public string GetRole()
        {
            if (_httpContextAccessor.HttpContext.User.Claims.Any())
            {
                return _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            }
            else
            {
                return null;
            }
        }
    }
}
