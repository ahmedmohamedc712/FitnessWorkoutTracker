using Application.Features.ExerciseProgresses.Delete;
using FastEndpoints;

namespace PublicApi.Endpoints.ExerciseProgresses.Delete;

public class DeleteExerciseProgressEndpoint(IDeleteExerciseProgressUseCase deleteExerciseProgressUseCase)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("api/exercise-progresses/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var exerciseProgressId = Route<Guid>("id");

        await deleteExerciseProgressUseCase.ExecuteAsync(exerciseProgressId);

        await SendNoContentAsync(cancellation: ct);
    }
}
