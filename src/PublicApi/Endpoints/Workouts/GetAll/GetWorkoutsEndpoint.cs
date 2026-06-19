using Application.Features.Workouts.GetAll;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.Workouts.GetAll
{
    public class GetWorkoutsEndpoint(IGetWorkoutsUseCase getWorkoutsUseCase) 
        : Endpoint<GetWorkoutsRequest, GetWorkoutsResponse>
    {
        public override void Configure()
        {
            Get("api/workouts");
        }

        public override async Task HandleAsync(GetWorkoutsRequest req, CancellationToken ct)
        {
            var userZone = HttpContext.Request.Headers[HeaderNames.TIME_ZONE_HEADER].ToString();

            var response = await getWorkoutsUseCase.ExecuteAsync(req, userZone);

            await SendAsync(response, cancellation: ct);
        }
    }
}
