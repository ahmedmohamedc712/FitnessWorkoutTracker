using Application.Abstraction;
using Application.Exceptions;
using Application.Features.Workouts.Create;
using NodaTime.TimeZones;

namespace Application.Features.ScheduledWorkouts.Cancel;

public class CancelScheduledWorkoutUseCase(IScheduledWorkoutRepository scheduledWorkoutRepository,
    ICurrentUserAccessor currentUserAccessor) : ICancelScheduledWorkoutUseCase
{
    public async Task ExecuteAsync(Guid scheduledWorkoutId)
    {
        var userId = currentUserAccessor.GetId();

        var scheduledWorkout = await scheduledWorkoutRepository
            .GetByIdWithWorkoutAndExerciseProgresses(scheduledWorkoutId, userId);

        if (scheduledWorkout is null)
            throw new NotFoundException($"Scheduled workout with ID `{scheduledWorkoutId}` not found.");

        scheduledWorkout.Cancel();

        await scheduledWorkoutRepository.SaveChangesAsync();
    }
}
