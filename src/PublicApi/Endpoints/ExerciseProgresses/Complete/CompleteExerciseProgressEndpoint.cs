using Application.Features.ExerciseProgresses.Complete;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ExerciseProgresses.Complete;

public class CompleteExerciseProgressEndpoint(ICompleteExerciseProgressUseCase completeExerciseProgressUseCase)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("api/exercise-progresses/{id}/complete");

        Description(b =>
        {
            b.WithSummary("Complete an exercise progress.");
            b.WithDescription("Mark the specified exercise progress as completed for the current user.");

            b.Produces(StatusCodes.Status204NoContent);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ExerciseProgressesTag);
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var exerciseProgressId = Route<Guid>("id");

        await completeExerciseProgressUseCase.ExecuteAsync(exerciseProgressId);

        await Send.NoContentAsync(cancellation: ct);
    }

}
