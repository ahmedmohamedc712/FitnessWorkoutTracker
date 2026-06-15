namespace Application.Features.ScheduledWorkouts.Schedule;

public interface IScheduleWorkoutUseCase
{
    Task<Guid> ExecuteAsync(DateTime sessionDate, Guid workoutId, string userZone);
}