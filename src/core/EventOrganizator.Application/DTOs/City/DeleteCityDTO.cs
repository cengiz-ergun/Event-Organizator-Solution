using EventOrganizator.Application.Features.Commands.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.DTOs.City
{
    public class DeleteCityDTO
    {
        public int? Id { get; set; }
    }

    public class DeleteCityDTOValidator : AbstractValidator<DeleteCityDTO>
    {
        public DeleteCityDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required!");
        }
    }
}
