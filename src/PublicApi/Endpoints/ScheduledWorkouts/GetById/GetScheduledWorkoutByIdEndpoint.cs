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

        Description(b =>
        {
            b.WithSummary("Get a scheduled workout by ID.");
            b.WithDescription("Retrieve a scheduled workout for the current user by its identifier.");

            b.Produces<ScheduledWorkoutDto>(StatusCodes.Status200OK);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ScheduledWorkoutsTag);
        });
    }

    public override async Task HandleAsync(GetScheduledWorkoutByIdEndpointRequest req, CancellationToken ct)
    {
        var scheduledWorkoutId = Route<Guid>("id");

        var scheduledWorkoutDto = await getScheduledWorkoutByIdUseCase.ExecuteAsync(scheduledWorkoutId, req.UserZone);

        await Send.OkAsync(scheduledWorkoutDto, cancellation: ct);
    }

}
