using Application.Features.Authentication.Login;
using FastEndpoints;
using FluentValidation;

namespace PublicApi.Endpoints.Login
{
    public class LoginCommandValidator : Validator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
