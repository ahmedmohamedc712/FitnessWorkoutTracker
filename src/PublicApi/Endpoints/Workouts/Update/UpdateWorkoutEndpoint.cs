using Application.Features.Workouts.Update;
using FastEndpoints;

namespace PublicApi.Endpoints.Workouts.Update;

public class UpdateWorkoutEndpoint(IUpdateWorkoutUseCase updateWorkoutUseCase) 
    : Endpoint<UpdateWorkoutRequest>
{
    public override void Configure()
    {
        Patch("api/workouts/{id}");
        Description(b =>
        {
            b.WithSummary("Update workout.");
            b.WithDescription("Update workout title and description for the current user.");

            b.Produces(StatusCodes.Status204NoContent);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.WorkoutsTag);
        });
    }

    public override async Task HandleAsync(UpdateWorkoutRequest req, CancellationToken ct)
    {
        var workoutId = Route<Guid>("id");

        await updateWorkoutUseCase.ExecuteAsync(workoutId, req);

        await Send.NoContentAsync(ct);
    }
}
