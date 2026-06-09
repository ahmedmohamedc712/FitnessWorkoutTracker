using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Workouts.GetAll
{
    public class WorkoutDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ExercisesCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
