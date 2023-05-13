using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Features.Commands.AppUser
{
    public class SignupUserCommandRequest: IRequest<SignupUserCommandResponse>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }


    public class SignupUserCommandRequestValidator : AbstractValidator<SignupUserCommandRequest>
    {
        public SignupUserCommandRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required!");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required!");
        }
    }
}
