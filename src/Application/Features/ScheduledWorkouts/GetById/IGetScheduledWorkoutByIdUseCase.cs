using Application.Features.Workouts.GetById;

namespace Application.Features.ScheduledWorkouts.GetById;

public interface IGetScheduledWorkoutByIdUseCase
{
    Task<ScheduledWorkoutDto> ExecuteAsync(Guid scheduledWorkoutId, string userZone);
}