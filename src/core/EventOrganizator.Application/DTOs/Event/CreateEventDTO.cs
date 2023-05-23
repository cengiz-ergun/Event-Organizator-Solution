using EventOrganizator.Application.DTOs.Member;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.DTOs.Event
{
    public class CreateEventDTO
    {
        public string? Name { get; set; }
        public string? Details { get; set; }
        public string? Address { get; set; }
        public int? NumberOfPeople { get; set; }
        public DateTime? Date { get; set; }

        public int? CategoryId { get; set; }
        public int? CityId { get; set; }
    }

    public class CreateEventDTOValidator : AbstractValidator<CreateEventDTO>
    {
        public CreateEventDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required!");
            RuleFor(x => x.Details).NotEmpty().WithMessage("Details is required!");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required!");
            RuleFor(x => x.NumberOfPeople).NotEmpty().WithMessage("NumberOfPeople is required!");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required!");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required!");
            RuleFor(x => x.CityId).NotEmpty().WithMessage("CityId is required!");
        }
    }
}
