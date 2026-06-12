namespace Application.Features.Authentication.Login;

public interface ILoginUseCase
{
    Task<LoginResponse> ExecuteAsync(LoginCommand command);
}
