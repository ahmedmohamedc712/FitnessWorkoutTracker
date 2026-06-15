namespace Application.Features.ScheduledWorkouts.Reschedule;

public interface IRescheduleWorkoutUseCase
{
    Task ExecuteAsync(Guid scheduledWorkoutId, string userZone, DateTime sessionDate);
}