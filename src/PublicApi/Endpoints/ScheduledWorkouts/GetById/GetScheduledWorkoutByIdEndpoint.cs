using Application.Features.ScheduledWorkouts.GetById;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ScheduledWorkouts.GetById;

public class GetScheduledWorkoutByIdEndpoint(
    IGetScheduledWorkoutByIdUseCase getScheduledWorkoutByIdUseCase) 
    : Endpoint<GetScheduledWorkoutByIdEndpointRequest, ScheduledWorkoutDto>
{
    public override void Configure()
    {
        Get("api/scheduled-workouts/{id}");
    }

    public override async Task HandleAsync(GetScheduledWorkoutByIdEndpointRequest req, CancellationToken ct)
    {
        var scheduledWorkoutId = Route<Guid>("id");

        var scheduledWorkoutDto = await getScheduledWorkoutByIdUseCase.ExecuteAsync(scheduledWorkoutId, req.UserZone);

        await Send.OkAsync(scheduledWorkoutDto, cancellation: ct);
    }

}
