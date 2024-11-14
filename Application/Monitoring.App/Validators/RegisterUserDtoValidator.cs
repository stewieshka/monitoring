using FluentValidation;
using Monitoring.App.Dtos;
using Monitoring.App.Dtos.Auth;

namespace Monitoring.App.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(128).WithMessage("Maximum length is 128 chars");

        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull()
            .MaximumLength(128).WithMessage("Maximum length is 128 chars");

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
    }
}