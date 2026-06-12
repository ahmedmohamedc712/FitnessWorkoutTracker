namespace Application.Features.ScheduledWorkouts.Schedule;

public interface IScheduleWorkoutUseCase
{
    Task<ScheduleWorkoutResponse> ExecuteAsync(DateTime sessionDate, Guid workoutId, string userZone);
}