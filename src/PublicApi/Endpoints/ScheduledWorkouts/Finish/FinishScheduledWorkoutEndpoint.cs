using Application.Features.ScheduledWorkouts.Finish;
using FastEndpoints;

namespace PublicApi.Endpoints.ScheduledWorkouts.Finish;

public class FinishScheduledWorkoutEndpoint(IFinishScheduledWorkoutUseCase finishScheduledWorkoutUseCase)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("api/scheduled-workouts/{id}/finish");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var scheduledWorkoutId = Route<Guid>("id");

        await finishScheduledWorkoutUseCase.ExecuteAsync(scheduledWorkoutId);

        await SendNoContentAsync(ct);
    }
}
