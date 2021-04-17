using Chat.Api.Dtos.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            
            CascadeMode = CascadeMode.Stop;
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Username).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.RepetedPassword).NotEmpty().Equal(c=>c.Password);
        }
    }
}
