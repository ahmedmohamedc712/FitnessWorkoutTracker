using Application.Features.ScheduledWorkouts.GetAll;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ScheduledWorkouts.GetAll;

public class GetScheduledWorkoutsEndpoint(IGetScheduledWorkoutsUseCase getScheduledExercisesUseCase)
    : EndpointWithoutRequest<GetScheduledExercisesResponse>
{
    public override void Configure()
    {
        Get("api/workouts/{workoutId}/scheduled-workouts");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userZone = HttpContext.Request.Headers[HeaderNames.TIME_ZONE_HEADER].ToString();

        var workoutId = Route<Guid>("workoutId");

        var response = await getScheduledExercisesUseCase.ExecuteAsync(workoutId, userZone);

        await SendAsync(response, cancellation: ct);
    }

}
