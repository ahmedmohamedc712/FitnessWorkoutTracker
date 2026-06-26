using Application.Features.ScheduledWorkouts.Reschedule;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ScheduledWorkouts.Reschedule;

public class RescheduleWorkoutEndpoint(IRescheduleWorkoutUseCase rescheduleWorkoutUseCase)
    : Endpoint<ScheduleWorkoutEndpointRequest>
{
    public override void Configure()
    {
        Post("api/scheduled-workouts/{id}/reschedule");

        Description(b =>
        {
            b.WithSummary("Reschedule a scheduled workout.");
            b.WithDescription("Reschedule an existing scheduled workout for the current user.");

            b.Produces(StatusCodes.Status204NoContent);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ScheduledWorkoutsTag);
        });
    }

    public override async Task HandleAsync(ScheduleWorkoutEndpointRequest req, CancellationToken ct)
    {
        var scheduledWorkoutId = Route<Guid>("id");

        await rescheduleWorkoutUseCase.ExecuteAsync(scheduledWorkoutId, req.UserZone, req.SessionDate);

        await Send.NoContentAsync(ct);
    }

}
