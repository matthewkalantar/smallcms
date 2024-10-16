using FluentValidation;

namespace SmallCMS.API.DTOs.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Wrong Email format.")
                .NotEmpty()
                .WithMessage("Missing Email address");


            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(4)
                .WithMessage("Password must be at least 4 characters long.");
        }
    }
}
