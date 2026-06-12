namespace Application.Features.ScheduledWorkouts.GetAll;

public class GetScheduledExercisesResponse
{
    public IEnumerable<ScheduledWorkoutDto> ScheduledWorkoutDtos {get; set; } = [];
}
