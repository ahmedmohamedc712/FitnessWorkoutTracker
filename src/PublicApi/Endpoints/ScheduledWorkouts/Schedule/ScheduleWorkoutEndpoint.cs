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
