using Application.Features.ExerciseProgresses.AddNote;
using FastEndpoints;

namespace PublicApi.Endpoints.ExerciseProgresses.AddNote;

public class AddNoteToExerciseProgressEndpoint(AddNoteToExerciseProgressUseCase addNoteToExerciseProgressUseCase)
    : Endpoint<AddNoteRequest>
{
    public override void Configure()
    {
        Post("api/exercises/add-note/{exerciseProgressId}");
    }

    public override async Task HandleAsync(AddNoteRequest req, CancellationToken ct)
    {
        var exerciseProgressId = Route<Guid>("exerciseProgressId");

        await addNoteToExerciseProgressUseCase.ExecuteAsync(exerciseProgressId, req);

        await SendOkAsync(ct);
    }
}
