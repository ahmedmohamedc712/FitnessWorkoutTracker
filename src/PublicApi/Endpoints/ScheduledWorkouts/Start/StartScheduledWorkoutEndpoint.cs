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

        Description(b =>
        {
            b.WithSummary("Start a scheduled workout.");
            b.WithDescription("Start an already existed scheduled workout for the current user.");

            b.Produces(StatusCodes.Status204NoContent);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ScheduledWorkoutsTag);
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var scheduledWorkoutId = Route<Guid>("id");

        await startScheduledWorkoutUseCase.ExecuteAsync(scheduledWorkoutId);

        await Send.NoContentAsync(ct);
    }

}
