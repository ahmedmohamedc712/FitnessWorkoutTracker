using Application.Features.Workouts.Delete;
using FastEndpoints;
namespace PublicApi.Endpoints.Workouts.Delete;

public class DeleteWorkoutEndpoint(IDeleteWorkoutUseCase deleteWorkoutUseCase)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("api/workouts/{id}");

        Description(b =>
        {
            b.WithSummary("Delete a workout");
            b.WithDescription("Delete a workout for the current user");

            b.Produces(StatusCodes.Status204NoContent);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);
            
            b.WithTags(Constants.Tags.WorkoutsTag);
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var workoutId = Route<Guid>("id");

        await deleteWorkoutUseCase.ExecuteAsync(workoutId);

        await Send.NoContentAsync(cancellation: ct);
    }
}
