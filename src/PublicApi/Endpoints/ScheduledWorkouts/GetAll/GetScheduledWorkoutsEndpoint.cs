using Application.Features.ScheduledWorkouts.GetAll;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ScheduledWorkouts.GetAll;

public class GetScheduledWorkoutsEndpoint(IGetScheduledWorkoutsUseCase getScheduledWorkoutsUseCase)
    : Endpoint<GetScheduledWorkoutsRequest, GetScheduledWorkoutsResponse>
{
    public override void Configure()
    {
        Get("api/workouts/{workoutId}/scheduled-workouts");
    }

    public override async Task HandleAsync(GetScheduledWorkoutsRequest req, CancellationToken ct)
    {
        var userZone = HttpContext.Request.Headers[HeaderNames.TIME_ZONE_HEADER].ToString();

        var workoutId = Route<Guid>("workoutId");

        var response = await getScheduledWorkoutsUseCase.ExecuteAsync(req, workoutId, userZone);

        await SendAsync(response, cancellation: ct);
    }

}
