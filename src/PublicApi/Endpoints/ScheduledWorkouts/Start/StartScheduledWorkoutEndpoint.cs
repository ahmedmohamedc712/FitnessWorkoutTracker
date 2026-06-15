using Application.Features.ScheduledWorkouts.Start;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ScheduledWorkouts.Start;

public class StartScheduledWorkoutEndpoint(IStartScheduledWorkoutUseCase startScheduledWorkoutUseCase)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("api/scheduled-workouts/{id}/start");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var scheduledWorkoutId = Route<Guid>("id");

        await startScheduledWorkoutUseCase.ExecuteAsync(scheduledWorkoutId);

        await SendNoContentAsync(ct);
    }

}
