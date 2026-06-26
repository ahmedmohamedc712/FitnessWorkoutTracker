using Application.Features.ExerciseProgresses.GetAll;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ExerciseProgresses.GetAll;

public class GetExerciseProgressesEndpoint(IGetExerciseProgressesUseCase getExerciseProgressesUseCase)
    : Endpoint<GetExerciseProgressesEndpointRequest, GetExerciseProgressesResponse>
{
    public override void Configure()
    {
        Get("api/scheduled-workouts/{scheduledWorkoutId}/exercise-progresses");

        Description(b =>
        {
            b.WithSummary("Get exercise progresses for a scheduled workout.");
            b.WithDescription("Retrieve exercise progress records for the specified scheduled workout.");

            b.Produces<GetExerciseProgressesResponse>(StatusCodes.Status200OK);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ExerciseProgressesTag);
        });
    }

    public override async Task HandleAsync(GetExerciseProgressesEndpointRequest req, CancellationToken ct)
    {
        var scheduledWorkoutId = Route<Guid>("scheduledWorkoutId");

        var response = await getExerciseProgressesUseCase
            .ExecuteAsync(scheduledWorkoutId, req.UserZone);

        await Send.OkAsync(response, cancellation: ct);
    }
}
