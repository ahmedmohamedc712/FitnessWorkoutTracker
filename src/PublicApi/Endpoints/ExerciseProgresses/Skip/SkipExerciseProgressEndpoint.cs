using Application.Features.ExerciseProgresses.Skip;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ExerciseProgresses.Skip;

public class SkipExerciseProgressEndpoint(ISkipExerciseProgressUseCase skipExerciseProgressUseCase)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("api/exercise-progresses/{id}/skip");

        Description(b =>
        {
            b.WithSummary("Skip an exercise progress.");
            b.WithDescription("Skip the specified exercise progress step for the current user.");

            b.Produces(StatusCodes.Status204NoContent);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ExerciseProgressesTag);
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var exerciseProgressId = Route<Guid>("id");

        await skipExerciseProgressUseCase.ExecuteAsync(exerciseProgressId);

        await Send.NoContentAsync(cancellation: ct);
    }

}
