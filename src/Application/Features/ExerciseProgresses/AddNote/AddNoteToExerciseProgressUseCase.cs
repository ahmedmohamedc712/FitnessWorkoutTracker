using Application.Abstraction;
using Application.Exceptions;
using Application.Features.Workouts.Create;

namespace Application.Features.ExerciseProgresses.AddNote;

public class AddNoteToExerciseProgressUseCase(IExerciseProgressRepository exerciseProgressRepository,
    ICurrentUserAccessor currentUserAccessor) : IAddNoteToExerciseProgressUseCase
{
    public async Task ExecuteAsync(Guid exerciseProgressId, AddNoteRequest req)
    {
        var userId = currentUserAccessor.GetId();

        var exerciseProgress = await exerciseProgressRepository.GetByIdWithNotes(exerciseProgressId, userId);

        if (exerciseProgress is null)
            throw new NotFoundException($"Exercise progress with ID `{exerciseProgressId}` not found.");

        exerciseProgress.AddNote(req.Content);
        await exerciseProgressRepository.SaveChangesAsync();
    }
}
