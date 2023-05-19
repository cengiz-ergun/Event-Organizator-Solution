using EventOrganizator.Application.Features.Commands.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.DTOs.City
{
    public class CreateCityDTO
    {
        public string? Name { get; set; }
    }

    public class CreateCityDTOValidator : AbstractValidator<CreateCityDTO>
    {
        public CreateCityDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required!");
        }
    }
}
