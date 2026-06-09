using Domain.Entities;

namespace Application.Features.ScheduledWorkouts.Schedule;

public class ScheduleWorkoutResponse
{
    public Guid Id { get; set; }
    public DateTime SessionDate { get; set; }
    public WorkoutStatus Status { get; set; }
}
