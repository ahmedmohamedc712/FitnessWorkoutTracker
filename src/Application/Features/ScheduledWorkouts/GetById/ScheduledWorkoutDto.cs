using Domain.Entities;

namespace Application.Features.ScheduledWorkouts.GetById;

public class ScheduledWorkoutDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public WorkoutStatus Status { get; set; } 
    public DateTime SessionDate { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
