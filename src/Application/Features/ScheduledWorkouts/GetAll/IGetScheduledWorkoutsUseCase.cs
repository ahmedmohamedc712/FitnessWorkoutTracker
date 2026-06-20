namespace Application.Features.ScheduledWorkouts.GetAll;

public interface IGetScheduledWorkoutsUseCase
{
    Task<GetScheduledWorkoutsResponse> ExecuteAsync(GetScheduledWorkoutsRequest req, Guid workoutId, string userZone);
}