using EventOrganizator.Application.Features.Commands.AppUser;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Features.Commands.Category
{
    public class CategoryCreateCommandRequest: IRequest<CategoryCreateCommandResponse>
    {
        public string? Name { get; set; }
    }

    public class CategoryCreateCommandRequestValidator : AbstractValidator<CategoryCreateCommandRequest>
    {
        public CategoryCreateCommandRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required!");
        }
    }
}
