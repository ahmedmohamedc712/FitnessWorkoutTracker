using Domain.Entities;

namespace Application.Features.ExerciseProgresses.GetAll;

public class ExerciseProgressDto
{
    public Guid Id { get;  set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int Sets { get;  set; }
    public int Reps { get;  set; }
    public ExerciseStatus Status { get;  set; }
    public DateTime? StartedAt { get;  set; }
    public DateTime? CompletedAt { get;  set; }
}
