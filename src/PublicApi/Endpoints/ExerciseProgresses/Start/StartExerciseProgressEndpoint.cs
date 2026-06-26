using Application.Features.ExerciseProgresses.Start;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ExerciseProgresses.Start;

public class StartExerciseProgressEndpoint(IStartExerciseProgressUseCase startExerciseProgressUseCase)
    : Endpoint<StartExerciseEndpointRequest, StartExerciseResponse>
{
    public override void Configure()
    {
        Post("api/exercise-progresses/{id}/start");

        Description(b =>
        {
            b.WithSummary("Start an exercise progress.");
            b.WithDescription("Start an existing exercise progress for the current user.");

            b.Produces<StartExerciseResponse>(StatusCodes.Status200OK);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ExerciseProgressesTag);
        });
    }

    public override async Task HandleAsync(StartExerciseEndpointRequest req, CancellationToken ct)
    {
        var exerciseProgressId = Route<Guid>("id");

        var request = new StartExerciseRequest
        {
            Sets = req.Sets,
            Reps = req.Reps
        };

        var response = await startExerciseProgressUseCase.ExecuteAsync(exerciseProgressId, request, req.UserZone);

        await Send.OkAsync(response, cancellation: ct);
    }

}
