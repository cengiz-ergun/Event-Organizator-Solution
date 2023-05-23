using EventOrganizator.Application.DTOs.City;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.DTOs.Member
{
    public class UpdateMemberDTO
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }

    public class UpdateMemberDTOValidator : AbstractValidator<UpdateMemberDTO>
    {
        public UpdateMemberDTOValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Old Password is required!");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New Password is required!");
        }
    }
}
