using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Application.Features.Commands.AppUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginUserCommandRequestValidator : AbstractValidator<LoginUserCommandRequest>
    {
        public LoginUserCommandRequestValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required!");
        }
    }
}
