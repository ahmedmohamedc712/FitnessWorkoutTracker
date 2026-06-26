using Application.Features.ScheduledWorkouts.Delete;
using FastEndpoints;

namespace PublicApi.Endpoints.ScheduledWorkouts.Delete;

public class DeleteScheduledWorkoutEndpoint(IDeleteScheduledWorkoutUseCase deleteScheduledWorkoutUseCase)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("api/scheduled-workouts/{id}");

                Description(b =>
        {
            b.WithSummary("Delete a scheduled workout.");
            b.WithDescription("Delete a scheduled workout for the current user.");

            b.Produces(StatusCodes.Status204NoContent);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ScheduledWorkoutsTag);
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var scheduledWorkoutId = Route<Guid>("id");

        await deleteScheduledWorkoutUseCase.ExecuteAsync(scheduledWorkoutId);

        await Send.NoContentAsync(ct);
    }

}
