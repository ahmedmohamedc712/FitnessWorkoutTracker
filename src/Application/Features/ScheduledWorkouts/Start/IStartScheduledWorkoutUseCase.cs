namespace Application.Features.ScheduledWorkouts.Start;

public interface IStartScheduledWorkoutUseCase
{
    Task ExecuteAsync(Guid scheduledWorkoutId);
}