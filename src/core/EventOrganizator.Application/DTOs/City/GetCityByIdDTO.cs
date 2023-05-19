using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.DTOs.City
{
    public class GetCityByIdDTO
    {
        public int Id { get; set; }
    }


    public class GetCityByIdDTOValidator : AbstractValidator<GetCityByIdDTO>
    {
        public GetCityByIdDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required!");
        }
    }
}
