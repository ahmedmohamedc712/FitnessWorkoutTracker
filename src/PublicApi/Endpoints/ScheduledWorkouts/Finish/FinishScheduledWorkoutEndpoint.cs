using Application.Features.ScheduledWorkouts.Finish;
using FastEndpoints;

namespace PublicApi.Endpoints.ScheduledWorkouts.Finish;

public class FinishScheduledWorkoutEndpoint(IFinishScheduledWorkoutUseCase finishScheduledWorkoutUseCase)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("api/scheduled-workouts/{id}/finish");

        Description(b =>
        {
            b.WithSummary("Finish a scheduled workout.");
            b.WithDescription("Finish an already started scheduled workout for the current user.");

            b.Produces(StatusCodes.Status204NoContent);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ScheduledWorkoutsTag);
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var scheduledWorkoutId = Route<Guid>("id");

        await finishScheduledWorkoutUseCase.ExecuteAsync(scheduledWorkoutId);

        await Send.NoContentAsync(ct);
    }
}
