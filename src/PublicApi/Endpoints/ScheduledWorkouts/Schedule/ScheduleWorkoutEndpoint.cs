using Application.Features.ScheduledWorkouts.Schedule;
using FastEndpoints;
using PublicApi.Constants;
using PublicApi.Endpoints.ScheduledWorkouts.GetById;

namespace PublicApi.Endpoints.ScheduledWorkouts;

public class ScheduleWorkoutEndpoint(IScheduleWorkoutUseCase scheduleWorkoutUseCase)
    : Endpoint<ScheduleWorkoutEndpointRequest>
{
    public override void Configure()
    {
        Post("api/workouts/{workoutId}/scheduled-workouts");

        Description(b =>
        {
            b.WithSummary("Schedule a workout.");
            b.WithDescription("Schedule a workout for the current user. and provide a location header for the new record.");

            b.Produces(StatusCodes.Status201Created);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ScheduledWorkoutsTag);
        });
    }

    public override async Task HandleAsync(ScheduleWorkoutEndpointRequest req, CancellationToken ct)
    {
        var workoutId = Route<Guid>("workoutId");

        var id = await scheduleWorkoutUseCase.ExecuteAsync(req.SessionDate, workoutId, req.UserZone);

        await Send.CreatedAtAsync<GetScheduledWorkoutByIdEndpoint>(
            new { id },
            generateAbsoluteUrl: true,
            cancellation: ct);
    }
}
