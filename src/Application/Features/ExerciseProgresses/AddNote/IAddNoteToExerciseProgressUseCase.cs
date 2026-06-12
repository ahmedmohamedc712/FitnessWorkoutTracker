namespace Application.Features.ExerciseProgresses.AddNote;

public interface IAddNoteToExerciseProgressUseCase
{
    Task ExecuteAsync(Guid exerciseProgressId, AddNoteRequest req);
}
