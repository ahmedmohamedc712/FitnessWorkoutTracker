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
    }

    public override async Task HandleAsync(ScheduleWorkoutEndpointRequest req, CancellationToken ct)
    {
        var scheduledWorkoutId = Route<Guid>("id");

        await rescheduleWorkoutUseCase.ExecuteAsync(scheduledWorkoutId, req.UserZone, req.SessionDate);

        await Send.NoContentAsync(ct);
    }

}
