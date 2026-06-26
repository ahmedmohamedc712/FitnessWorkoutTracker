using Application.Features.ExerciseProgresses.Delete;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ExerciseProgresses.Delete;

public class DeleteExerciseProgressEndpoint(IDeleteExerciseProgressUseCase deleteExerciseProgressUseCase)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("api/exercise-progresses/{id}");

        Description(b =>
        {
            b.WithSummary("Delete an exercise progress.");
            b.WithDescription("Delete an exercise progress record for the current user.");

            b.Produces(StatusCodes.Status204NoContent);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ExerciseProgressesTag);
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var exerciseProgressId = Route<Guid>("id");

        await deleteExerciseProgressUseCase.ExecuteAsync(exerciseProgressId);

        await Send.NoContentAsync(cancellation: ct);
    }
}
