namespace Application.Features.Authentication.Signup;

public interface ISignupUseCase
{
    Task<SignupResult> ExecuteAsync(SignupCommand command);
}
