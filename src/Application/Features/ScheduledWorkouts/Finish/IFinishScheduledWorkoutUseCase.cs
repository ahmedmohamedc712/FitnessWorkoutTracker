namespace Application.Features.ScheduledWorkouts.Finish;

public interface IFinishScheduledWorkoutUseCase
{
    Task ExecuteAsync(Guid scheduledWorkoutId);
}