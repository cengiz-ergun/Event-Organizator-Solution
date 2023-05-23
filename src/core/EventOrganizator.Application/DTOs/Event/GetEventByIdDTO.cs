using EventOrganizator.Application.DTOs.City;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.DTOs.Event
{
    public class GetEventByIdDTO
    {
        public int Id { get; set; }
    }

    public class GetEventByIdDTOValidator : AbstractValidator<GetEventByIdDTO>
    {
        public GetEventByIdDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required!");
        }
    }
}
