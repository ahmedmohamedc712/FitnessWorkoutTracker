using Application.Abstraction;
using Application.Exceptions;
using Application.Features.Workouts.Create;
using NodaTime.TimeZones;

namespace Application.Features.ScheduledWorkouts.Finish;

public class FinishScheduledWorkoutUseCase(IScheduledWorkoutRepository scheduledWorkoutRepository,
    ICurrentUserAccessor currentUserAccessor) : IFinishScheduledWorkoutUseCase
{
    public async Task ExecuteAsync(Guid scheduledWorkoutId)
    {
        var userId = currentUserAccessor.GetId();

        var scheduledWorkout = await scheduledWorkoutRepository
            .GetByIdWithWorkoutAndExerciseProgresses(scheduledWorkoutId, userId);

        if (scheduledWorkout is null)
            throw new NotFoundException($"Scheduled workout with ID `{scheduledWorkoutId}` not found.");

        scheduledWorkout.Finish();

        await scheduledWorkoutRepository.SaveChangesAsync();
    }
}
