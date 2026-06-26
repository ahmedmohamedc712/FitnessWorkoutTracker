using Application.Features.ScheduledWorkouts.Cancel;
using FastEndpoints;

namespace PublicApi.Endpoints.ScheduledWorkouts.Cancel;

public class CancelScheduledWorkoutEndpoint(ICancelScheduledWorkoutUseCase cancelScheduledWorkoutUseCase)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("api/scheduled-workouts/{id}/cancel");

        Description(b =>
        {
            b.WithSummary("Cancel a scheduled workout.");
            b.WithDescription("Cancel an already existed non-completed scheduled workout for the current user.");

            b.Produces(StatusCodes.Status204NoContent);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ScheduledWorkoutsTag);
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var scheduledWorkoutId = Route<Guid>("id");

        await cancelScheduledWorkoutUseCase.ExecuteAsync(scheduledWorkoutId);

        await Send.NoContentAsync(ct);
    }
}
