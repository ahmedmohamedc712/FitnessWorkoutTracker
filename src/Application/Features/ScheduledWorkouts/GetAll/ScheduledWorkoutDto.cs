using Domain.Entities;

namespace Application.Features.ScheduledWorkouts.GetAll;

public class ScheduledWorkoutDto
{
    public Guid Id { get; set; }
    public DateTime SessionDate { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public WorkoutStatus Status { get; set; }
}
