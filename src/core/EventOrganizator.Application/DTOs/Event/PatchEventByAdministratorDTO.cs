using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.DTOs.Event
{
    public class PatchEventByAdministratorDTO
    {
        public int? EventStatus { get; set; }
    }
    public class PatchEventByAdministratorDTOValidator : AbstractValidator<PatchEventByAdministratorDTO>
    {
        public PatchEventByAdministratorDTOValidator()
        {
            RuleFor(x => x.EventStatus).NotEmpty().WithMessage("EventStatus is required!");
        }
    }
}
