using Application.Features.ExerciseProgresses.AddNote;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ExerciseProgresses.AddNote;

public class AddNoteToExerciseProgressEndpoint(IAddNoteToExerciseProgressUseCase addNoteToExerciseProgressUseCase)
    : Endpoint<AddNoteRequest>
{
    public override void Configure()
    {
        Post("api/exercise-progresses/{id}/notes");

        Description(b =>
        {
            b.WithSummary("Add a note to exercise progress.");
            b.WithDescription("Attach a note to the specified exercise progress record.");

            b.Produces(StatusCodes.Status204NoContent);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ExerciseProgressesTag);
        });
    }

    public override async Task HandleAsync(AddNoteRequest req, CancellationToken ct)
    {
        var exerciseProgressId = Route<Guid>("id");

        await addNoteToExerciseProgressUseCase.ExecuteAsync(exerciseProgressId, req);

        await Send.NoContentAsync(ct);
    }
}
