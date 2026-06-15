using Application.Abstraction;
using Application.Exceptions;

namespace Application.Features.ExerciseProgresses.Delete;

public class DeleteExerciseProgressUseCase(IExerciseProgressRepository exerciseProgressRepository,
    ICurrentUserAccessor currentUserAccessor) : IDeleteExerciseProgressUseCase
{
    public async Task ExecuteAsync(Guid exerciseProgressId)
    {
        var userId = currentUserAccessor.GetId();

        var exerciseProgress = await exerciseProgressRepository
            .GetByIdWithScheduledWorkout(exerciseProgressId, userId);

        if (exerciseProgress is null)
            throw new NotFoundException($"Exercise progress `{exerciseProgressId}` not found.");

        exerciseProgressRepository.Delete(exerciseProgress);
        await exerciseProgressRepository.SaveChangesAsync();
    }
}
