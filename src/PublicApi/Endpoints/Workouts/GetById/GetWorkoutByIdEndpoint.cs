using Application.Features.Workouts.GetById;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.Workouts.GetById;

public class GetWorkoutByIdEndpoint(IGetWorkoutByIdUseCase getWorkoutByIdUseCase)
    : Endpoint<GetWorkoutByIdEndpointRequest, GetWorkoutByIdResponse>
{
    public override void Configure()
    {
        Get("api/workouts/{id}");

        Description(b =>
        {
            b.WithSummary("Get workout by ID");
            b.WithDescription("Retrieve a workout for the current user by its identifier.");

            b.Produces<GetWorkoutByIdResponse>(StatusCodes.Status200OK);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);
            
            b.WithTags(Constants.Tags.WorkoutsTag);
        });
    }

    public override async Task HandleAsync(GetWorkoutByIdEndpointRequest req, CancellationToken ct)
    {
        var workoutId = Route<Guid>("id");

        var response = await getWorkoutByIdUseCase.ExecuteAsync(workoutId, req.UserZone);

        await Send.OkAsync(response, cancellation: ct);
    }
}
