namespace Application.Features.ScheduledWorkouts.Start;

public interface IStartScheduledWorkoutUseCase
{
    Task<StartScheduledWorkoutResponse> ExecuteAsync(Guid scheduledWorkoutId, string userZone);
}