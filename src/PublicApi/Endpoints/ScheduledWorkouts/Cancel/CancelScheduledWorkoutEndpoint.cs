using Application.Features.ScheduledWorkouts.Cancel;
using FastEndpoints;

namespace PublicApi.Endpoints.ScheduledWorkouts.Cancel;

public class CancelScheduledWorkoutEndpoint(ICancelScheduledWorkoutUseCase cancelScheduledWorkoutUseCase)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("api/scheduled-workouts/{id}/cancel");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var scheduledWorkoutId = Route<Guid>("id");

        await cancelScheduledWorkoutUseCase.ExecuteAsync(scheduledWorkoutId);

        await SendNoContentAsync(ct);
    }
}
