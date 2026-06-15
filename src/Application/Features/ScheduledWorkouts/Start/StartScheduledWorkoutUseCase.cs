using Application.Abstraction;
using Application.Exceptions;
using Application.Features.Workouts.Create;

namespace Application.Features.ScheduledWorkouts.Start;

public class StartScheduledWorkoutUseCase(IScheduledWorkoutRepository scheduledWorkoutRepository,
    ICurrentUserAccessor currentUserAccessor) : IStartScheduledWorkoutUseCase
{
    public async Task ExecuteAsync(Guid scheduledWorkoutId)
    {
            
        var userId = currentUserAccessor.GetId();

        var scheduledWorkout = await scheduledWorkoutRepository
            .GetByIdWithWorkoutThenExercises(scheduledWorkoutId, userId);

        if (scheduledWorkout is null)
            throw new NotFoundException($"Scheduled workout with ID `{scheduledWorkoutId}` not found.");

        scheduledWorkout.Start();

        await scheduledWorkoutRepository.SaveChangesAsync();
    }
}
