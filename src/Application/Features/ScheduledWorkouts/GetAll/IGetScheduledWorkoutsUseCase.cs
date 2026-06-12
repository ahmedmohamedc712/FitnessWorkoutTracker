namespace Application.Features.ScheduledWorkouts.GetAll;

public interface IGetScheduledWorkoutsUseCase
{
    Task<GetScheduledExercisesResponse> ExecuteAsync(Guid workoutId, string userZone);
}