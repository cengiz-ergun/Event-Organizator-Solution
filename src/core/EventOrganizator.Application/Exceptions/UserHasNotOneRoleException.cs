using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Exceptions
{
    public class UserHasNotOneRoleException : Exception
    {
        public UserHasNotOneRoleException() : base("User should have 1 role!")
        {
        }

        public UserHasNotOneRoleException(string? message) : base(message)
        {
        }

        public UserHasNotOneRoleException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
