using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.DTOs.Event
{
    public class PatchEventByMemberDTO
    {
        public int? Id { get; set; }
        public string? Address { get; set; }
        public int? NumberOfPeople { get; set; }
    }

    public class PatchEventByMemberDTOValidator : AbstractValidator<PatchEventByMemberDTO>
    {
        public PatchEventByMemberDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required!");
        }
    }
}
