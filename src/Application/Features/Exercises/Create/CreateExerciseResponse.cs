using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Exercises.Create
{
    public class CreateExerciseResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid WorkoutId { get; set; }
    }
}
