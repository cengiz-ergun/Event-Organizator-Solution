using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Abstractions.Services
{
    public interface IWorkingContext
    {
        string GetFirstName();
        string GetLastName();
        string GetEmail();
        string GetRole();
    }
}
