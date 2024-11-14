using FluentValidation;
using Monitoring.App.Dtos;
using Monitoring.App.Dtos.Auth;

namespace Monitoring.App.Validators;

public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email should not be null")
            .NotEmpty().WithMessage("Email is required")
            .MaximumLength(128).WithMessage("Maximum length is 128 chars");
        
        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password should not be null")
            .NotEmpty().WithMessage("Password is required");
    }
}