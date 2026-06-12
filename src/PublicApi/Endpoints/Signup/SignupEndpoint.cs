using Application.Features.Authentication.Signup;
using FastEndpoints;

namespace PublicApi.Endpoints.Signup;

public class SignupEndpoint(ISignupUseCase signupUseCase) : Endpoint<SignupRequest, SignupResult>
{
    public override void Configure()
    {
        Post("api/auth/signup");
        AllowAnonymous();
    }

    public async override Task HandleAsync(SignupRequest req, CancellationToken ct)
    {
        var result = await signupUseCase.ExecuteAsync(
            new SignupCommand(req.Username, req.Email, req.Password));

        await SendAsync(new SignupResult()
        {
            Token = result.Token
        });
    }
}
