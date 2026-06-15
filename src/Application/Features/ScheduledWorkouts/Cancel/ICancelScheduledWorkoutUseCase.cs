namespace Application.Features.ScheduledWorkouts.Cancel;

public interface ICancelScheduledWorkoutUseCase
{
    Task ExecuteAsync(Guid scheduledWorkoutId);
}