using EventOrganizator.Application.DTOs.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<SignupUserResponseDTO> SignupUserAsync(SignupUserDTO model);
    }
}
