namespace Application.Features.ScheduledWorkouts.GetAll;

public class GetScheduledWorkoutsResponse
{
    public IEnumerable<ScheduledWorkoutDto> ScheduledWorkoutDtos { get; set; } = []; 
}
