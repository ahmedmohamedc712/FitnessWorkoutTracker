using Application.Features.Authentication.Login;
using FastEndpoints;

namespace PublicApi.Endpoints.Login
{
    public class LoginEndpoint(ILoginUseCase loginUseCase) : Endpoint<LoginCommand, LoginResponse>
    {
        public override void Configure()
        {
            Post("api/auth/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginCommand req, CancellationToken ct)
        {
            var response = await loginUseCase.ExecuteAsync(req);

            await SendAsync(response, cancellation: ct);
        }
    }
}
