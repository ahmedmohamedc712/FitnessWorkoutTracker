using Application.Features.Workouts.GetAll;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.Workouts.Get
{
    public class GetWorkoutsEndpoint(GetWorkoutsUseCase getWorkoutsUseCase) 
        : EndpointWithoutRequest<GetWorkoutsResponse>
    {
        public override void Configure()
        {
            Get("api/workouts");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var userZone = HttpContext.Request.Headers[HeaderNames.TIME_ZONE_HEADER].ToString();

            var response = await getWorkoutsUseCase.ExecuteAsync(userZone);

            await SendAsync(response, cancellation: ct);
        }
    }
}
