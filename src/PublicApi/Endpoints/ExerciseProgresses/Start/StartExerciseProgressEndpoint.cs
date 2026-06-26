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
