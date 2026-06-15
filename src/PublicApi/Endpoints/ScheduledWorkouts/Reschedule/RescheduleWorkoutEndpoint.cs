using Application.Features.ScheduledWorkouts.Reschedule;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ScheduledWorkouts.Reschedule;

public class RescheduleWorkoutEndpoint(IRescheduleWorkoutUseCase rescheduleWorkoutUseCase)
    : Endpoint<ScheduleWorkoutRequest>
{
    public override void Configure()
    {
        Post("api/scheduled-workouts/{id}/reschedule");
    }

    public override async Task HandleAsync(ScheduleWorkoutRequest req, CancellationToken ct)
    {
        var userZone = HttpContext.Request.Headers[HeaderNames.TIME_ZONE_HEADER].ToString();

        var scheduledWorkoutId = Route<Guid>("id");

        await rescheduleWorkoutUseCase.ExecuteAsync(scheduledWorkoutId, userZone, req.SessionDate);

        await SendNoContentAsync(ct);
    }

}
