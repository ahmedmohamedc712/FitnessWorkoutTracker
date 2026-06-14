using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Exercises.GetAll
{
    public class ExerciseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
